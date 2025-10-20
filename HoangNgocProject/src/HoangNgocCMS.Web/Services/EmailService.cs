using OrchardCore.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HoangNgocCMS.Web.Services
{
    public interface ICustomEmailService
    {
        Task SendWelcomeEmailAsync(string email, string name);
        Task SendEmailConfirmationAsync(string email, string name, string confirmationUrl);
        Task SendPasswordResetEmailAsync(string email, string resetUrl);
        Task SendJobApplicationConfirmationAsync(string email, string name, string jobTitle, string companyName);
        Task SendApplicationStatusUpdateAsync(string email, string name, string jobTitle, string status);
        Task SendEventRegistrationConfirmationAsync(string email, string name, string eventTitle, DateTime eventDate);
        Task SendCourseEnrollmentConfirmationAsync(string email, string name, string courseTitle);
    }

    public class CustomEmailService : ICustomEmailService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<CustomEmailService> _logger;
        private readonly EmailSettings _emailSettings;

        public CustomEmailService(
            IEmailService emailService,
            ILogger<CustomEmailService> logger,
            IOptions<EmailSettings> emailSettings)
        {
            _emailService = emailService;
            _logger = logger;
            _emailSettings = emailSettings.Value;
        }

        public async Task SendWelcomeEmailAsync(string email, string name)
        {
            try
            {
                var subject = "Welcome to HoangNgoc - Your Professional Journey Starts Here!";
                var body = GetWelcomeEmailTemplate(name);

                var message = new MailMessage
                {
                    To = email,
                    Subject = subject,
                    Body = body,
                    IsHtmlBody = true
                };

                await _emailService.SendAsync(message);
                _logger.LogInformation($"Welcome email sent to {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send welcome email to {email}");
                throw;
            }
        }

        public async Task SendEmailConfirmationAsync(string email, string name, string confirmationUrl)
        {
            try
            {
                var subject = "Confirm Your Email Address - HoangNgoc";
                var body = GetEmailConfirmationTemplate(name, confirmationUrl);

                var message = new MailMessage
                {
                    To = email,
                    Subject = subject,
                    Body = body,
                    IsHtmlBody = true
                };

                await _emailService.SendAsync(message);
                _logger.LogInformation($"Email confirmation sent to {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email confirmation to {email}");
                throw;
            }
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetUrl)
        {
            try
            {
                var subject = "Reset Your Password - HoangNgoc";
                var body = GetPasswordResetTemplate(resetUrl);

                var message = new MailMessage
                {
                    To = email,
                    Subject = subject,
                    Body = body,
                    IsHtmlBody = true
                };

                await _emailService.SendAsync(message);
                _logger.LogInformation($"Password reset email sent to {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send password reset email to {email}");
                throw;
            }
        }

        public async Task SendJobApplicationConfirmationAsync(string email, string name, string jobTitle, string companyName)
        {
            try
            {
                var subject = $"Application Received - {jobTitle} at {companyName}";
                var body = GetJobApplicationConfirmationTemplate(name, jobTitle, companyName);

                var message = new MailMessage
                {
                    To = email,
                    Subject = subject,
                    Body = body,
                    IsHtmlBody = true
                };

                await _emailService.SendAsync(message);
                _logger.LogInformation($"Job application confirmation sent to {email} for {jobTitle}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send job application confirmation to {email}");
                throw;
            }
        }

        public async Task SendApplicationStatusUpdateAsync(string email, string name, string jobTitle, string status)
        {
            try
            {
                var subject = $"Application Update - {jobTitle}";
                var body = GetApplicationStatusUpdateTemplate(name, jobTitle, status);

                var message = new MailMessage
                {
                    To = email,
                    Subject = subject,
                    Body = body,
                    IsHtmlBody = true
                };

                await _emailService.SendAsync(message);
                _logger.LogInformation($"Application status update sent to {email} for {jobTitle}: {status}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send application status update to {email}");
                throw;
            }
        }

        public async Task SendEventRegistrationConfirmationAsync(string email, string name, string eventTitle, DateTime eventDate)
        {
            try
            {
                var subject = $"Event Registration Confirmed - {eventTitle}";
                var body = GetEventRegistrationConfirmationTemplate(name, eventTitle, eventDate);

                var message = new MailMessage
                {
                    To = email,
                    Subject = subject,
                    Body = body,
                    IsHtmlBody = true
                };

                await _emailService.SendAsync(message);
                _logger.LogInformation($"Event registration confirmation sent to {email} for {eventTitle}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send event registration confirmation to {email}");
                throw;
            }
        }

        public async Task SendCourseEnrollmentConfirmationAsync(string email, string name, string courseTitle)
        {
            try
            {
                var subject = $"Course Enrollment Confirmed - {courseTitle}";
                var body = GetCourseEnrollmentConfirmationTemplate(name, courseTitle);

                var message = new MailMessage
                {
                    To = email,
                    Subject = subject,
                    Body = body,
                    IsHtmlBody = true
                };

                await _emailService.SendAsync(message);
                _logger.LogInformation($"Course enrollment confirmation sent to {email} for {courseTitle}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send course enrollment confirmation to {email}");
                throw;
            }
        }

        // Email Templates
        private string GetWelcomeEmailTemplate(string name)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <title>Welcome to HoangNgoc</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f8f9fa; padding: 30px; border-radius: 0 0 10px 10px; }}
        .button {{ display: inline-block; background: #007bff; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎉 Welcome to HoangNgoc!</h1>
            <p>Your Professional Journey Starts Here</p>
        </div>
        <div class='content'>
            <h2>Hello {name}!</h2>
            <p>Thank you for joining HoangNgoc, Vietnam's premier professional platform. We're excited to help you advance your career and connect with amazing opportunities.</p>
            
            <h3>What you can do now:</h3>
            <ul>
                <li>🔍 <strong>Explore Jobs:</strong> Browse thousands of job opportunities from top companies</li>
                <li>📚 <strong>Take Courses:</strong> Enhance your skills with our professional development courses</li>
                <li>🎯 <strong>Attend Events:</strong> Network with industry professionals at our events</li>
                <li>📰 <strong>Stay Updated:</strong> Read the latest industry news and insights</li>
            </ul>
            
            <a href='{_emailSettings.WebsiteUrl}/jobs' class='button'>Start Exploring Jobs</a>
            
            <p>If you have any questions, our support team is here to help. Just reply to this email!</p>
            
            <p>Best regards,<br>The HoangNgoc Team</p>
        </div>
        <div class='footer'>
            <p>© 2024 HoangNgoc. All rights reserved.</p>
            <p>Visit us at <a href='{_emailSettings.WebsiteUrl}'>{_emailSettings.WebsiteUrl}</a></p>
        </div>
    </div>
</body>
</html>";
        }

        private string GetEmailConfirmationTemplate(string name, string confirmationUrl)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <title>Confirm Your Email</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #28a745; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f8f9fa; padding: 30px; border-radius: 0 0 10px 10px; }}
        .button {{ display: inline-block; background: #28a745; color: white; padding: 15px 30px; text-decoration: none; border-radius: 5px; margin: 20px 0; font-weight: bold; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>✉️ Confirm Your Email</h1>
        </div>
        <div class='content'>
            <h2>Hello {name}!</h2>
            <p>Thank you for registering with HoangNgoc. To complete your registration and start using all our features, please confirm your email address.</p>
            
            <a href='{confirmationUrl}' class='button'>Confirm Email Address</a>
            
            <p>If the button doesn't work, copy and paste this link into your browser:</p>
            <p style='word-break: break-all; color: #007bff;'>{confirmationUrl}</p>
            
            <p><strong>This link will expire in 24 hours.</strong></p>
            
            <p>If you didn't create an account with us, please ignore this email.</p>
            
            <p>Best regards,<br>The HoangNgoc Team</p>
        </div>
        <div class='footer'>
            <p>© 2024 HoangNgoc. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GetPasswordResetTemplate(string resetUrl)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <title>Reset Your Password</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #dc3545; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f8f9fa; padding: 30px; border-radius: 0 0 10px 10px; }}
        .button {{ display: inline-block; background: #dc3545; color: white; padding: 15px 30px; text-decoration: none; border-radius: 5px; margin: 20px 0; font-weight: bold; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🔐 Reset Your Password</h1>
        </div>
        <div class='content'>
            <p>You requested to reset your password for your HoangNgoc account.</p>
            
            <a href='{resetUrl}' class='button'>Reset Password</a>
            
            <p>If the button doesn't work, copy and paste this link into your browser:</p>
            <p style='word-break: break-all; color: #007bff;'>{resetUrl}</p>
            
            <p><strong>This link will expire in 1 hour.</strong></p>
            
            <p>If you didn't request a password reset, please ignore this email. Your password will remain unchanged.</p>
            
            <p>Best regards,<br>The HoangNgoc Team</p>
        </div>
        <div class='footer'>
            <p>© 2024 HoangNgoc. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GetJobApplicationConfirmationTemplate(string name, string jobTitle, string companyName)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <title>Application Received</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #17a2b8; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f8f9fa; padding: 30px; border-radius: 0 0 10px 10px; }}
        .job-info {{ background: white; padding: 20px; border-radius: 5px; margin: 20px 0; border-left: 4px solid #17a2b8; }}
        .button {{ display: inline-block; background: #17a2b8; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>✅ Application Received!</h1>
        </div>
        <div class='content'>
            <h2>Hello {name}!</h2>
            <p>Thank you for applying! We've successfully received your application and wanted to confirm the details:</p>
            
            <div class='job-info'>
                <h3>📋 Application Details</h3>
                <p><strong>Position:</strong> {jobTitle}</p>
                <p><strong>Company:</strong> {companyName}</p>
                <p><strong>Applied:</strong> {DateTime.Now:MMMM dd, yyyy 'at' HH:mm}</p>
            </div>
            
            <h3>What happens next?</h3>
            <ol>
                <li>The hiring team will review your application</li>
                <li>If you're a good fit, they'll contact you directly</li>
                <li>You can track your application status in your profile</li>
            </ol>
            
            <a href='{_emailSettings.WebsiteUrl}/account/profile' class='button'>View Application Status</a>
            
            <p>We'll keep you updated on any changes to your application status.</p>
            
            <p>Good luck!<br>The HoangNgoc Team</p>
        </div>
        <div class='footer'>
            <p>© 2024 HoangNgoc. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GetApplicationStatusUpdateTemplate(string name, string jobTitle, string status)
        {
            var statusColor = status.ToLower() switch
            {
                "accepted" => "#28a745",
                "rejected" => "#dc3545",
                "interview" => "#ffc107",
                _ => "#17a2b8"
            };

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <title>Application Update</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: {statusColor}; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f8f9fa; padding: 30px; border-radius: 0 0 10px 10px; }}
        .status-badge {{ background: {statusColor}; color: white; padding: 8px 16px; border-radius: 20px; display: inline-block; font-weight: bold; }}
        .button {{ display: inline-block; background: {statusColor}; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>📬 Application Update</h1>
        </div>
        <div class='content'>
            <h2>Hello {name}!</h2>
            <p>We have an update on your job application:</p>
            
            <p><strong>Position:</strong> {jobTitle}</p>
            <p><strong>Status:</strong> <span class='status-badge'>{status.ToUpper()}</span></p>
            
            <a href='{_emailSettings.WebsiteUrl}/account/profile' class='button'>View Full Details</a>
            
            <p>Thank you for your interest in this position.</p>
            
            <p>Best regards,<br>The HoangNgoc Team</p>
        </div>
        <div class='footer'>
            <p>© 2024 HoangNgoc. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GetEventRegistrationConfirmationTemplate(string name, string eventTitle, DateTime eventDate)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <title>Event Registration Confirmed</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #6f42c1; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f8f9fa; padding: 30px; border-radius: 0 0 10px 10px; }}
        .event-info {{ background: white; padding: 20px; border-radius: 5px; margin: 20px 0; border-left: 4px solid #6f42c1; }}
        .button {{ display: inline-block; background: #6f42c1; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎉 Registration Confirmed!</h1>
        </div>
        <div class='content'>
            <h2>Hello {name}!</h2>
            <p>Great news! Your registration for the following event has been confirmed:</p>
            
            <div class='event-info'>
                <h3>📅 Event Details</h3>
                <p><strong>Event:</strong> {eventTitle}</p>
                <p><strong>Date:</strong> {eventDate:MMMM dd, yyyy}</p>
                <p><strong>Time:</strong> {eventDate:HH:mm}</p>
            </div>
            
            <p>We're excited to see you there! You'll receive a reminder email closer to the event date.</p>
            
            <a href='{_emailSettings.WebsiteUrl}/account/profile' class='button'>View My Events</a>
            
            <p>If you need to cancel your registration, please contact us as soon as possible.</p>
            
            <p>See you at the event!<br>The HoangNgoc Team</p>
        </div>
        <div class='footer'>
            <p>© 2024 HoangNgoc. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GetCourseEnrollmentConfirmationTemplate(string name, string courseTitle)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <title>Course Enrollment Confirmed</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #fd7e14; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f8f9fa; padding: 30px; border-radius: 0 0 10px 10px; }}
        .course-info {{ background: white; padding: 20px; border-radius: 5px; margin: 20px 0; border-left: 4px solid #fd7e14; }}
        .button {{ display: inline-block; background: #fd7e14; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>📚 Enrollment Confirmed!</h1>
        </div>
        <div class='content'>
            <h2>Hello {name}!</h2>
            <p>Congratulations! You've successfully enrolled in:</p>
            
            <div class='course-info'>
                <h3>🎓 Course Details</h3>
                <p><strong>Course:</strong> {courseTitle}</p>
                <p><strong>Enrolled:</strong> {DateTime.Now:MMMM dd, yyyy}</p>
            </div>
            
            <p>You can now access your course materials and start learning right away!</p>
            
            <a href='{_emailSettings.WebsiteUrl}/account/profile' class='button'>Access My Courses</a>
            
            <p>Happy learning!<br>The HoangNgoc Team</p>
        </div>
        <div class='footer'>
            <p>© 2024 HoangNgoc. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }
    }

    public class EmailSettings
    {
        public string WebsiteUrl { get; set; } = "https://hoangngoc.com";
        public string SupportEmail { get; set; } = "support@hoangngoc.com";
        public string NoReplyEmail { get; set; } = "noreply@hoangngoc.com";
    }
}