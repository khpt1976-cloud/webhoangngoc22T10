using OrchardCore.Media.Fields;
namespace HoangNgoc.Core.Models
{
    public enum TransactionStatus
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Expired = 3,
        RequiresReview = 4,
        Cancelled = 5
    }

    public enum TransactionType
    {
        Credit = 1,  // Add funds
        Debit = 2    // Deduct funds
    }
}