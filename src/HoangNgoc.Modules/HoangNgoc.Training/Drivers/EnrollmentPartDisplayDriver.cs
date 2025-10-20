using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using HoangNgoc.Training.Models;

namespace HoangNgoc.Training.Drivers
{
    public class EnrollmentPartDisplayDriver : ContentPartDisplayDriver<EnrollmentPart>
    {
        public override IDisplayResult Display(EnrollmentPart part, BuildPartDisplayContext context)
        {
            return Initialize<EnrollmentPart>("EnrollmentPart", m => m = part)
                .Location("Detail", "Content:5")
                .Location("Summary", "Content:5");
        }

        public override IDisplayResult Edit(EnrollmentPart part, BuildPartEditorContext context)
        {
            return Initialize<EnrollmentPart>("EnrollmentPart_Edit", m => m = part);
        }

        public override async Task<IDisplayResult> UpdateAsync(EnrollmentPart part, UpdatePartEditorContext context)
        {
            await context.Updater.TryUpdateModelAsync(part, Prefix);
            return Edit(part, context);
        }
    }
}