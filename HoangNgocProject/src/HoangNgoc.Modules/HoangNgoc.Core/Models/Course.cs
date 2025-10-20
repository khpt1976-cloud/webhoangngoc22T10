using OrchardCore.Media.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.Course.Models
{
    public class Course : ContentPart
    {
        public TextField Title { get; set; } = new();
        public HtmlField Description { get; set; } = new();
        public TextField ShortDescription { get; set; } = new();
        public TextField Instructor { get; set; } = new();
        public TextField InstructorBio { get; set; } = new();
        public MediaField InstructorPhoto { get; set; } = new();
        public TextField Category { get; set; } = new();
        public TextField Level { get; set; } = new(); // Beginner, Intermediate, Advanced
        public TextField Language { get; set; } = new();
        public NumericField Duration { get; set; } = new(); // in hours
        public NumericField Price { get; set; } = new();
        public NumericField DiscountPrice { get; set; } = new();
        public TextField Currency { get; set; } = new();
        public BooleanField IsFree { get; set; } = new();
        public BooleanField IsFeatured { get; set; } = new();
        public BooleanField IsPublished { get; set; } = new();
        public MediaField Thumbnail { get; set; } = new();
        public MediaField PreviewVideo { get; set; } = new();
        public TextField Requirements { get; set; } = new();
        public TextField WhatYouWillLearn { get; set; } = new();
        public TextField Tags { get; set; } = new();
        public NumericField MaxStudents { get; set; } = new();
        public NumericField EnrolledStudents { get; set; } = new();
        public NumericField Rating { get; set; } = new();
        public NumericField ReviewCount { get; set; } = new();
        public DateTimeField StartDate { get; set; } = new();
        public DateTimeField EndDate { get; set; } = new();
        public BooleanField HasCertificate { get; set; } = new();
        public TextField CertificateTemplate { get; set; } = new();
        public DateTimeField CreatedDate { get; set; } = new();
        public DateTimeField LastModified { get; set; } = new();
    }

    public class CourseEnrollment : ContentPart
    {
        public string Id => ContentItem?.ContentItemId ?? string.Empty;
        public TextField CourseId { get; set; } = new();
        public TextField StudentId { get; set; } = new();
        public TextField StudentName { get; set; } = new();
        public TextField StudentEmail { get; set; } = new();
        public DateTimeField EnrollmentDate { get; set; } = new();
        public NumericField Progress { get; set; } = new(); // 0-100%
        public BooleanField IsCompleted { get; set; } = new();
        public DateTimeField CompletionDate { get; set; } = new();
        public NumericField Grade { get; set; } = new();
        public BooleanField HasCertificate { get; set; } = new();
        public TextField CertificateUrl { get; set; } = new();
        public DateTimeField LastAccessDate { get; set; } = new();
        public NumericField TimeSpent { get; set; } = new(); // in minutes
        public TextField Status { get; set; } = new(); // Active, Completed, Dropped, Suspended
        public NumericField Rating { get; set; } = new();
        public HtmlField Review { get; set; } = new();
        public DateTimeField ReviewDate { get; set; } = new();
    }
}