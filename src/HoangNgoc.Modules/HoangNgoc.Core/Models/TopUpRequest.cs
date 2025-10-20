using System;

namespace HoangNgoc.Core.Models
{
    public class TopUpRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string Status { get; set; } = "Pending";
        public string TransactionId { get; set; }
    }
}