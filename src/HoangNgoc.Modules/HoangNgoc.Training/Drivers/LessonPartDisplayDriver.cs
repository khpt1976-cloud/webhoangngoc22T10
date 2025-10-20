using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using HoangNgoc.Training.Models;

namespace HoangNgoc.Training.Drivers
{
    public class LessonPartDisplayDriver : ContentPartDisplayDriver<LessonPart>
    {
        public override IDisplayResult Display(LessonPart part, BuildPartDisplayContext context)
        {
            return Initialize<LessonPart>("LessonPart", m => m = part)
                .Location("Detail", "Content:5")
                .Location("Summary", "Content:5");
        }

        public override IDisplayResult Edit(LessonPart part, BuildPartEditorContext context)
        {
            return Initialize<LessonPart>("LessonPart_Edit", m => m = part);
        }

        public override async Task<IDisplayResult> UpdateAsync(LessonPart part, UpdatePartEditorContext context)
        {
            await context.Updater.TryUpdateModelAsync(part, Prefix);
            return Edit(part, context);
        }
    }
}