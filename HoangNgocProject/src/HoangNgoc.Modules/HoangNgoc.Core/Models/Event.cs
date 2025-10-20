using OrchardCore.Media.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.Event.Models
{
    public class Event : ContentPart
    {
        public TextField Title { get; set; } = new();
        public HtmlField Description { get; set; } = new();
        public TextField ShortDescription { get; set; } = new();
        public TextField Category { get; set; } = new();
        public TextField EventType { get; set; } = new(); // Conference, Workshop, Seminar, Webinar
        public DateTimeField StartDate { get; set; } = new();
        public DateTimeField EndDate { get; set; } = new();
        public TextField StartTime { get; set; } = new();
        public TextField EndTime { get; set; } = new();
        public TextField TimeZone { get; set; } = new();
        public TextField Location { get; set; } = new();
        public TextField Address { get; set; } = new();
        public TextField City { get; set; } = new();
        public TextField Country { get; set; } = new();
        public BooleanField IsOnline { get; set; } = new();
        public TextField OnlineUrl { get; set; } = new();
        public TextField Platform { get; set; } = new(); // Zoom, Teams, Meet
        public TextField Organizer { get; set; } = new();
        public TextField OrganizerEmail { get; set; } = new();
        public TextField OrganizerPhone { get; set; } = new();
        public MediaField OrganizerLogo { get; set; } = new();
        public NumericField Price { get; set; } = new();
        public TextField Currency { get; set; } = new();
        public BooleanField IsFree { get; set; } = new();
        public BooleanField IsFeatured { get; set; } = new();
        public MediaField Banner { get; set; } = new();
        public MediaField Thumbnail { get; set; } = new();
        public TextField Tags { get; set; } = new();
        public NumericField MaxAttendees { get; set; } = new();
        public NumericField RegisteredAttendees { get; set; } = new();
        public BooleanField RequiresApproval { get; set; } = new();
        public DateTimeField RegistrationDeadline { get; set; } = new();
        public BooleanField IsPublished { get; set; } = new();
        public TextField Status { get; set; } = new(); // Draft, Published, Cancelled, Completed
        public DateTimeField CreatedDate { get; set; } = new();
        public DateTimeField LastModified { get; set; } = new();
    }

    public class EventRegistration : ContentPart
    {
        public TextField EventId { get; set; } = new();
        public TextField AttendeeId { get; set; } = new();
        public TextField AttendeeName { get; set; } = new();
        public TextField AttendeeEmail { get; set; } = new();
        public TextField AttendeePhone { get; set; } = new();
        public TextField Company { get; set; } = new();
        public TextField JobTitle { get; set; } = new();
        public DateTimeField RegistrationDate { get; set; } = new();
        public TextField Status { get; set; } = new(); // Registered, Confirmed, Attended, NoShow, Cancelled
        public BooleanField PaymentRequired { get; set; } = new();
        public TextField PaymentStatus { get; set; } = new(); // Pending, Paid, Refunded
        public TextField PaymentId { get; set; } = new();
        public NumericField AmountPaid { get; set; } = new();
        public DateTimeField PaymentDate { get; set; } = new();
        public BooleanField IsApproved { get; set; } = new();
        public DateTimeField ApprovalDate { get; set; } = new();
        public TextField ApprovedBy { get; set; } = new();
        public HtmlField SpecialRequests { get; set; } = new();
        public BooleanField AttendedEvent { get; set; } = new();
        public DateTimeField CheckInTime { get; set; } = new();
        public TextField QRCode { get; set; } = new();
        public NumericField Rating { get; set; } = new();
        public HtmlField Feedback { get; set; } = new();
        public DateTimeField FeedbackDate { get; set; } = new();
    }
}