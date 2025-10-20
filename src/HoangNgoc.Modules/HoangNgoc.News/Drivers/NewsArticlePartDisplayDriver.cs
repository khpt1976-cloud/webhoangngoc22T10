using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using HoangNgoc.News.Models;

namespace HoangNgoc.News.Drivers;

public class NewsArticlePartDisplayDriver : ContentPartDisplayDriver<NewsArticlePart>
{
    public override IDisplayResult Display(NewsArticlePart part, BuildPartDisplayContext context)
    {
        return Initialize<NewsArticlePart>("NewsArticlePart", m => m = part)
            .Location("Detail", "Content:5")
            .Location("Summary", "Content:5");
    }

    public override IDisplayResult Edit(NewsArticlePart part, BuildPartEditorContext context)
    {
        return Initialize<NewsArticlePart>("NewsArticlePart_Edit", m => m = part);
    }

    public override async Task<IDisplayResult> UpdateAsync(NewsArticlePart part, UpdatePartEditorContext context)
    {
        await context.Updater.TryUpdateModelAsync(part, Prefix);
        return Edit(part, context);
    }
}