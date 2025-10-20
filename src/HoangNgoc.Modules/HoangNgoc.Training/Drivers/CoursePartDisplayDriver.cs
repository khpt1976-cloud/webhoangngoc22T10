using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using HoangNgoc.Training.Models;

namespace HoangNgoc.Training.Drivers
{
    public class CoursePartDisplayDriver : ContentPartDisplayDriver<CoursePart>
    {
        public override IDisplayResult Display(CoursePart part, BuildPartDisplayContext context)
        {
            return Initialize<CoursePart>("CoursePart", m => m = part)
                .Location("Detail", "Content:5")
                .Location("Summary", "Content:5");
        }

        public override IDisplayResult Edit(CoursePart part, BuildPartEditorContext context)
        {
            return Initialize<CoursePart>("CoursePart_Edit", m => m = part);
        }

        public override async Task<IDisplayResult> UpdateAsync(CoursePart part, UpdatePartEditorContext context)
        {
            await context.Updater.TryUpdateModelAsync(part, Prefix);
            return Edit(part, context);
        }
    }
}