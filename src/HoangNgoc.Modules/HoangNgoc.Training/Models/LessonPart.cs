using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;
using OrchardCore.Media.Fields;

namespace HoangNgoc.Training.Models
{
    public class LessonPart : ContentPart
    {
        public TextField LessonNumber { get; set; } = new();
        public new HtmlField Content { get; set; } = new();
        public TextField Duration { get; set; } = new();
        public TextField VideoUrl { get; set; } = new();
        public MediaField VideoFile { get; set; } = new();
        public MediaField Materials { get; set; } = new();
        public TextField LearningObjectives { get; set; } = new();
        public BooleanField IsPreview { get; set; } = new();
        public BooleanField IsActive { get; set; } = new();
        public NumericField SortOrder { get; set; } = new();
        public ContentPickerField Course { get; set; } = new();
        public TextField Quiz { get; set; } = new();
        public TextField Assignment { get; set; } = new();
        public NumericField PassingScore { get; set; } = new();
    }
}