using OrchardCore.Media.Fields;
using System;

namespace HoangNgoc.Core.Models
{
    public class WithdrawRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string Status { get; set; } = "Pending";
        public string TransactionId { get; set; }
    }
}