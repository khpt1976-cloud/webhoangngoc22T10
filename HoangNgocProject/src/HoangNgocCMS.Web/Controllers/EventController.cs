using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using YesSql;
using System.Linq;
using YesSqlSession = YesSql.ISession;
using OrchardCore.ContentManagement.Records;
using HoangNgoc.Event.Services;
using HoangNgocCMS.Web.ViewModels;

namespace HoangNgocCMS.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly YesSqlSession _session;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IEventRegistrationService _eventRegistrationService;

        public EventController(
            IContentManager contentManager,
            IContentItemDisplayManager contentItemDisplayManager,
            YesSqlSession session,
            IUpdateModelAccessor updateModelAccessor,
            IEventRegistrationService eventRegistrationService)
        {
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            _session = session;
            _updateModelAccessor = updateModelAccessor;
            _eventRegistrationService = eventRegistrationService;
        }

        // GET: /events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var eventItem = await _contentManager.GetAsync(id);
            
            if (eventItem == null || !eventItem.Published || eventItem.ContentType != "Event")
            {
                return NotFound();
            }

            // Build display shape for rendering
            var shape = await _contentItemDisplayManager.BuildDisplayAsync(eventItem, _updateModelAccessor.ModelUpdater);

            // Create EventViewModel from ContentItem
            var eventViewModel = new EventViewModel
            {
                Id = eventItem.ContentItemId,
                Title = eventItem.Content.Event.Title?.Text ?? eventItem.DisplayText,
                Description = eventItem.Content.Event.Description?.Text ?? "",
                StartDate = eventItem.Content.Event.StartDate?.Value ?? DateTime.Now,
                EndDate = eventItem.Content.Event.EndDate?.Value ?? DateTime.Now,
                Location = eventItem.Content.Event.Location?.Text ?? "",
                Price = eventItem.Content.Event.Price?.Value ?? 0,
                MaxAttendees = eventItem.Content.Event.MaxAttendees?.Value ?? 0,
                Category = eventItem.Content.Event.Category?.Text ?? "",
                OrganizerName = eventItem.Content.Event.OrganizerName?.Text ?? "",
                OrganizerEmail = eventItem.Content.Event.OrganizerEmail?.Text ?? "",
                OrganizerPhone = eventItem.Content.Event.OrganizerPhone?.Text ?? "",
                IsActive = eventItem.Published,
                CreatedUtc = eventItem.CreatedUtc ?? DateTime.UtcNow,
                ModifiedUtc = eventItem.ModifiedUtc ?? DateTime.UtcNow
            };

            // Get related events
            var relatedEvents = await GetRelatedEventsAsync(eventItem);

            // Get attendees count
            var attendeesCount = await _eventRegistrationService.GetAttendeesCountAsync(id);
            var maxAttendees = eventItem.Content.Event.MaxAttendees?.Value ?? 0;

            var viewModel = new EventDetailsViewModel
            {
                Event = eventViewModel,
                RelatedEvents = relatedEvents,
                IsUserLoggedIn = User.Identity.IsAuthenticated,
                IsUserRegistered = User.Identity.IsAuthenticated ? 
                    await _eventRegistrationService.IsUserRegisteredAsync(id, User.Identity.Name) : false,
                AttendeesCount = attendeesCount,
                MaxAttendees = maxAttendees,
                IsFull = maxAttendees > 0 && attendeesCount >= maxAttendees,
                RegistrationStatus = GetRegistrationStatus(eventItem, attendeesCount, maxAttendees)
            };

            return View(viewModel);
        }

        // POST: /api/events/{id}/register
        [HttpPost]
        [Route("api/events/{id}/register")]
        [Authorize]
        public async Task<IActionResult> Register(string id, [FromBody] EventRegistrationModel model)
        {
            try
            {
                var eventItem = await _contentManager.GetAsync(id);
                if (eventItem == null || !eventItem.Published || eventItem.ContentType != "Event")
                {
                    return NotFound(new { success = false, message = "Event not found" });
                }

                var userId = User.Identity.Name;

                // Check if already registered
                if (await _eventRegistrationService.IsUserRegisteredAsync(id, userId))
                {
                    return BadRequest(new { success = false, message = "You are already registered for this event" });
                }

                // Check event capacity
                var attendeesCount = await _eventRegistrationService.GetAttendeesCountAsync(id);
                var maxAttendees = eventItem.Content.Event.MaxAttendees?.Value ?? 0;
                
                if (maxAttendees > 0 && attendeesCount >= maxAttendees)
                {
                    return BadRequest(new { success = false, message = "Event is full" });
                }

                // Check registration deadline
                var eventDate = eventItem.Content.Event.StartDate?.Value;
                if (eventDate.HasValue && eventDate.Value < DateTime.UtcNow)
                {
                    return BadRequest(new { success = false, message = "Event registration has closed" });
                }

                // Create registration
                var registrationId = await _eventRegistrationService.RegisterUserAsync(new EventRegistrationCreateModel
                {
                    EventId = id,
                    UserId = userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Company = model.Company,
                    JobTitle = model.JobTitle,
                    DietaryRequirements = model.DietaryRequirements,
                    SpecialRequirements = model.SpecialRequirements
                });

                return Ok(new
                {
                    success = true,
                    registrationId = registrationId,
                    message = "Successfully registered for the event"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while registering for the event" });
            }
        }

        // DELETE: /api/events/{id}/register
        [HttpDelete]
        [Route("api/events/{id}/register")]
        [Authorize]
        public async Task<IActionResult> CancelRegistration(string id)
        {
            try
            {
                var eventItem = await _contentManager.GetAsync(id);
                if (eventItem == null || !eventItem.Published || eventItem.ContentType != "Event")
                {
                    return NotFound(new { success = false, message = "Event not found" });
                }

                var userId = User.Identity.Name;

                // Check if user is registered
                if (!await _eventRegistrationService.IsUserRegisteredAsync(id, userId))
                {
                    return BadRequest(new { success = false, message = "You are not registered for this event" });
                }

                // Check cancellation deadline (e.g., 24 hours before event)
                var eventDate = eventItem.Content.Event.StartDate?.Value;
                if (eventDate.HasValue && eventDate.Value.AddHours(-24) < DateTime.UtcNow)
                {
                    return BadRequest(new { success = false, message = "Cancellation deadline has passed" });
                }

                await _eventRegistrationService.CancelRegistrationAsync(id);

                return Ok(new { success = true, message = "Registration cancelled successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while cancelling registration" });
            }
        }

        // GET: /api/events/{id}/attendees
        [HttpGet]
        [Route("api/events/{id}/attendees")]
        public async Task<IActionResult> GetAttendees(string id)
        {
            try
            {
                var eventItem = await _contentManager.GetAsync(id);
                if (eventItem == null || !eventItem.Published || eventItem.ContentType != "Event")
                {
                    return NotFound(new { success = false, message = "Event not found" });
                }

                var attendees = await _eventRegistrationService.GetEventAttendeesAsync(id);

                return Ok(new
                {
                    success = true,
                    data = attendees.Select(a => new
                    {
                        name = a.AttendeeName?.Text ?? string.Empty,
                        company = a.Company?.Text ?? string.Empty,
                        jobTitle = a.JobTitle?.Text ?? string.Empty,
                        registrationDate = a.RegistrationDate?.Value ?? DateTime.MinValue
                    }),
                    count = attendees.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while fetching attendees" });
            }
        }

        // Helper methods
        private async Task<List<EventViewModel>> GetRelatedEventsAsync(ContentItem eventItem)
        {
            var category = eventItem.Content.Event.Category?.Text;
            var location = eventItem.Content.Event.Location?.Text;

            var allEvents = await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "Event" && 
                           x.Published && 
                           x.ContentItemId != eventItem.ContentItemId)
                .Take(10)
                .ListAsync();

            // Filter by category or location in memory
            var relatedEvents = allEvents.Take(3).ToList();
            var eventViewModels = new List<EventViewModel>();

            foreach (var relatedEvent in relatedEvents)
            {
                var eventViewModel = new EventViewModel
                {
                    Id = relatedEvent.ContentItemId,
                    Title = relatedEvent.Content.Event.Title?.Text ?? relatedEvent.DisplayText,
                    Description = relatedEvent.Content.Event.Description?.Text ?? "",
                    StartDate = relatedEvent.Content.Event.StartDate?.Value ?? DateTime.Now,
                    EndDate = relatedEvent.Content.Event.EndDate?.Value ?? DateTime.Now,
                    Location = relatedEvent.Content.Event.Location?.Text ?? "",
                    Price = relatedEvent.Content.Event.Price?.Value ?? 0,
                    MaxAttendees = relatedEvent.Content.Event.MaxAttendees?.Value ?? 0,
                    Category = relatedEvent.Content.Event.Category?.Text ?? "",
                    OrganizerName = relatedEvent.Content.Event.OrganizerName?.Text ?? "",
                    OrganizerEmail = relatedEvent.Content.Event.OrganizerEmail?.Text ?? "",
                    OrganizerPhone = relatedEvent.Content.Event.OrganizerPhone?.Text ?? "",
                    IsActive = relatedEvent.Published,
                    CreatedUtc = relatedEvent.CreatedUtc ?? DateTime.UtcNow,
                    ModifiedUtc = relatedEvent.ModifiedUtc ?? DateTime.UtcNow
                };
                eventViewModels.Add(eventViewModel);
            }

            return eventViewModels;
        }

        private string GetRegistrationStatus(ContentItem eventItem, int attendeesCount, int maxAttendees)
        {
            var eventDate = eventItem.Content.Event.StartDate?.Value;
            
            if (eventDate.HasValue && eventDate.Value < DateTime.UtcNow)
            {
                return "past";
            }

            if (maxAttendees > 0 && attendeesCount >= maxAttendees)
            {
                return "full";
            }

            return "available";
        }
    }
}