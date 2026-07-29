using Interfaces_Abstraction.Enums;
using Interfaces_Abstraction.Interfaces;
using Interfaces_Abstraction.Models;

namespace Interfaces_Abstraction.Services;

public class PaymentService
{
    private readonly List<Payment> _processedPayments = new();

    public void ProcessPayment(Payment payment)

    {
        if (_processedPayments.Any(p => p.PaymentId == payment.PaymentId))
        {
            Console.WriteLine($"A payment with ID {payment.PaymentId} already exists. Please use a different ID.");
            return;
        }
        IPaymentProcessor processor = payment.PaymentMethod switch
        {
            PaymentMethod.Stripe => new StripePaymentProcessor(),
            PaymentMethod.Securenet => new SecurenetPaymentProcessor(),
            PaymentMethod.Paypal => new PaypalPaymentProcessor(),

            _ => throw new NotSupportedException("Unsupported payment method.")
        };

        processor.ProcessPayment(payment);

        _processedPayments.Add(payment);
    }

    public void RefundPayment(int paymentId)
    {
        Payment? payment = GetPaymentById(paymentId);

        if (payment == null)
        {
            Console.WriteLine($"No processed payment found with ID {paymentId}.");
            return;
        }

        IPaymentProcessor processor = payment.PaymentMethod switch
        {
            PaymentMethod.Stripe => new StripePaymentProcessor(),
            PaymentMethod.Securenet => new SecurenetPaymentProcessor(),
            PaymentMethod.Paypal => new PaypalPaymentProcessor(),
            _ => throw new NotSupportedException("Unsupported payment method.")
        };

        processor.RefundPayment(payment);
    }

    public Payment? GetPaymentById(int paymentId)
    {
        return _processedPayments.FirstOrDefault(p => p.PaymentId == paymentId);
    }

    public List<Payment> GetAllPayments()
    {
        return _processedPayments;
    }
}