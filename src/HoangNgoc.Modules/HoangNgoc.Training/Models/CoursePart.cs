using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;
using OrchardCore.Media.Fields;
using OrchardCore.Taxonomies.Fields;

namespace HoangNgoc.Training.Models
{
    public class CoursePart : ContentPart
    {
        public TextField CourseCode { get; set; } = new();
        public HtmlField Description { get; set; } = new();
        public TextField Duration { get; set; } = new();
        public TextField Level { get; set; } = new();
        public NumericField Price { get; set; } = new();
        public NumericField MaxStudents { get; set; } = new();
        public NumericField CurrentStudents { get; set; } = new();
        public BooleanField IsActive { get; set; } = new();
        public BooleanField IsFeatured { get; set; } = new();
        public DateTimeField StartDate { get; set; } = new();
        public DateTimeField EndDate { get; set; } = new();
        public MediaField ThumbnailImage { get; set; } = new();
        public TaxonomyField Category { get; set; } = new();
        public TaxonomyField Tags { get; set; } = new();
        public TextField Prerequisites { get; set; } = new();
        public TextField LearningOutcomes { get; set; } = new();
        public TextField Instructor { get; set; } = new();
        public TextField InstructorBio { get; set; } = new();
        public MediaField InstructorPhoto { get; set; } = new();
    }
}