using OrchardCore.Media.Fields;
using System;

namespace HoangNgoc.Core.Models
{
    public class TransferRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string ToUserEmail { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string Status { get; set; } = "Pending";
        public string TransactionId { get; set; }
    }
}