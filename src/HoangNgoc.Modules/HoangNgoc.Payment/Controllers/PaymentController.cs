using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using HoangNgoc.Payment.Services;
using HoangNgoc.Payment.Models;
using HoangNgoc.Payment.ViewModels;

namespace HoangNgoc.Payment.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IContentManager _contentManager;

        public PaymentController(IPaymentService paymentService, IContentManager contentManager)
        {
            _paymentService = paymentService;
            _contentManager = contentManager;
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPayment(string paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetPaymentsByOrder(string orderId)
        {
            var payments = await _paymentService.GetPaymentsByOrderIdAsync(orderId);
            return Ok(payments);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetPaymentsByStatus(string status)
        {
            var payments = await _paymentService.GetPaymentsByStatusAsync(status);
            return Ok(payments);
        }

        [HttpGet("customer/{email}")]
        public async Task<IActionResult> GetPaymentsByCustomer(string email)
        {
            var payments = await _paymentService.GetPaymentsByCustomerEmailAsync(email);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentPartViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var paymentPart = new PaymentPart();
            paymentPart.PaymentId.Text = model.PaymentId;
            paymentPart.OrderId.Text = model.OrderId;
            paymentPart.Amount.Value = model.Amount;
            paymentPart.Currency.Text = model.Currency;
            paymentPart.PaymentMethod.Text = model.PaymentMethod;
            paymentPart.PaymentStatus.Text = model.PaymentStatus;
            paymentPart.TransactionId.Text = model.TransactionId;
            paymentPart.PaymentGateway.Text = model.PaymentGateway;
            paymentPart.PaymentDate.Value = model.PaymentDate;
            paymentPart.CustomerEmail.Text = model.CustomerEmail;
            paymentPart.CustomerPhone.Text = model.CustomerPhone;
            paymentPart.BillingAddress.Text = model.BillingAddress;
            paymentPart.PaymentDescription.Text = model.PaymentDescription;
            paymentPart.PaymentNotes.Text = model.PaymentNotes;

            var isValid = await _paymentService.ValidatePaymentAsync(paymentPart);
            if (!isValid)
                return BadRequest("Invalid payment data");

            var payment = await _paymentService.CreatePaymentAsync(paymentPart);
            return CreatedAtAction(nameof(GetPayment), new { paymentId = model.PaymentId }, payment);
        }

        [HttpPut("{paymentId}/status")]
        public async Task<IActionResult> UpdatePaymentStatus(string paymentId, [FromBody] string status)
        {
            var success = await _paymentService.UpdatePaymentStatusAsync(paymentId, status);
            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpPost("{paymentId}/refund")]
        public async Task<IActionResult> ProcessRefund(string paymentId, [FromBody] RefundRequest request)
        {
            var success = await _paymentService.ProcessRefundAsync(paymentId, request.Amount, request.Reason);
            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentPayments(int count = 10)
        {
            var payments = await _paymentService.GetRecentPaymentsAsync(count);
            return Ok(payments);
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalPayments(DateTime fromDate, DateTime toDate)
        {
            var total = await _paymentService.GetTotalPaymentAmountAsync(fromDate, toDate);
            return Ok(new { Total = total, FromDate = fromDate, ToDate = toDate });
        }
    }

    public class RefundRequest
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}