using Interfaces_Abstraction.Enums;
using Interfaces_Abstraction.Helpers;
using Interfaces_Abstraction.Models;
using Stripe;
using Interfaces_Abstraction.Configuration;

namespace Interfaces_Abstraction.Services;

public class StripePaymentProcessor : PaymentProcessorBase
{
    public override void ProcessPayment(Payment payment)
    {
        StripeConfiguration.ApiKey = StripeSettings.SecretKey;

        if (!Validate(payment))
        {
            payment.Status = PaymentStatus.Failed;
            Console.WriteLine("Payment validation failed.");
            return;
        }

        try
        {
            // Read Card Details

            string testToken = ConsoleHelper.ReadString("Enter Test Token (e.g. tok_visa): ")!;

            var paymentMethodOptions = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Token = testToken   // Number/ExpMonth/ExpYear/Cvc ki jagah sirf ye
                }
            };

            

            var paymentMethodService = new PaymentMethodService();

            Stripe.PaymentMethod paymentMethod = paymentMethodService.Create(paymentMethodOptions);

            Console.WriteLine();
            Console.WriteLine("Payment Method Created Successfully.");
            Console.WriteLine($"Payment Method Id : {paymentMethod.Id}");

            // Create Payment Intent
            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = (long)(payment.Amount * 100),
                Currency = "usd",
                PaymentMethod = paymentMethod.Id,
                Confirm = true,
                PaymentMethodTypes = new List<string> { "card" },   // ye line add karo
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = false
                }
            };

            var paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Create(paymentIntentOptions);

            Console.WriteLine();
            Console.WriteLine("Payment Successful!");
            Console.WriteLine($"Payment Intent ID : {paymentIntent.Id}");
            Console.WriteLine($"Stripe Status     : {paymentIntent.Status}");

            payment.Status = paymentIntent.Status == "succeeded"
                ? PaymentStatus.Completed
                : PaymentStatus.Pending;
        }
        catch (StripeException ex)
        {
            payment.Status = PaymentStatus.Failed;
            Console.WriteLine($"Stripe Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            payment.Status = PaymentStatus.Failed;
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public override void RefundPayment(Payment payment)
{
    StripeConfiguration.ApiKey = "sk_test_...";

    try
    {
        var refundOptions = new RefundCreateOptions
        {
            PaymentIntent = payment.StripePaymentIntentId,
        };

        var refundService = new RefundService();
        Refund refund = refundService.Create(refundOptions);

        Console.WriteLine();
        Console.WriteLine("Refund Successful!");
        Console.WriteLine($"Refund ID     : {refund.Id}");
        Console.WriteLine($"Refund Status : {refund.Status}");

        payment.Status = refund.Status == "succeeded"
            ? PaymentStatus.Refunded
            : PaymentStatus.Pending;
    }
    catch (StripeException ex)
    {
        Console.WriteLine($"Stripe Refund Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
}