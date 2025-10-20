using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.DisplayManagement.ModelBinding;
using HoangNgoc.Payment.Models;
using HoangNgoc.Payment.ViewModels;

namespace HoangNgoc.Payment.Drivers
{
    public class PaymentPartDisplayDriver : ContentPartDisplayDriver<PaymentPart>
    {
        public override IDisplayResult Display(PaymentPart part, BuildPartDisplayContext context)
        {
            return Initialize<PaymentPartViewModel>(GetDisplayShapeType(context), viewModel =>
            {
                viewModel.PaymentPart = part;
                viewModel.ContentItem = part.ContentItem;
            })
            .Location("Detail", "Content:10")
            .Location("Summary", "Content:10");
        }

        public override IDisplayResult Edit(PaymentPart part, BuildPartEditorContext context)
        {
            return Initialize<PaymentPartViewModel>(GetEditorShapeType(context), viewModel =>
            {
                viewModel.PaymentPart = part;
                viewModel.ContentItem = part.ContentItem;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(PaymentPart part, UpdatePartEditorContext context)
        {
            var viewModel = new PaymentPartViewModel();
            
            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.PaymentId.Text = viewModel.PaymentId;
                part.OrderId.Text = viewModel.OrderId;
                part.Amount.Value = viewModel.Amount;
                part.Currency.Text = viewModel.Currency;
                part.PaymentMethod.Text = viewModel.PaymentMethod;
                part.PaymentStatus.Text = viewModel.PaymentStatus;
                part.TransactionId.Text = viewModel.TransactionId;
                part.PaymentGateway.Text = viewModel.PaymentGateway;
                part.PaymentDate.Value = viewModel.PaymentDate;
                part.CustomerEmail.Text = viewModel.CustomerEmail;
                part.CustomerPhone.Text = viewModel.CustomerPhone;
                part.BillingAddress.Text = viewModel.BillingAddress;
                part.PaymentDescription.Text = viewModel.PaymentDescription;
                part.PaymentNotes.Text = viewModel.PaymentNotes;
                part.IsRefunded.Value = viewModel.IsRefunded;
                part.RefundAmount.Value = viewModel.RefundAmount;
                part.RefundDate.Value = viewModel.RefundDate;
            }

            return Edit(part, context);
        }
    }
}