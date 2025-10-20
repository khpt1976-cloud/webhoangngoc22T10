using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.Contents;

namespace HoangNgoc.Training.AdminMenu
{
    public class TrainingAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public TrainingAdminMenu(IStringLocalizer<TrainingAdminMenu> localizer)
        {
            S = localizer;
        }

        public ValueTask BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return ValueTask.CompletedTask;
            }

            builder
                .Add(S["Training"], "10", training => training
                    .AddClass("training").Id("training")
                    .Add(S["Courses"], "1", courses => courses
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "Course" })
                        .Permission(CommonPermissions.AccessContentApi)
                        .LocalNav())
                    .Add(S["Lessons"], "2", lessons => lessons
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "Lesson" })
                        .Permission(CommonPermissions.AccessContentApi)
                        .LocalNav())
                    .Add(S["Enrollments"], "3", enrollments => enrollments
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "Enrollment" })
                        .Permission(CommonPermissions.AccessContentApi)
                        .LocalNav())
                    .Add(S["Course Categories"], "4", categories => categories
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "CourseCategory" })
                        .Permission(CommonPermissions.AccessContentApi)
                        .LocalNav())
                    .Add(S["Course Tags"], "5", tags => tags
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "CourseTag" })
                        .Permission(CommonPermissions.AccessContentApi)
                        .LocalNav())
                );

            return ValueTask.CompletedTask;
        }
    }
}