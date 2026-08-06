using Interfaces_Abstraction.Models;
using Interfaces_Abstraction.Enums;
using Interfaces_Abstraction.Interfaces;
using System;


namespace Interfaces_Abstraction.Services;

public class SecurenetPaymentProcessor : PaymentProcessorBase
{
    public override void ProcessPayment(Payment payment)
    {
        Console.WriteLine("Processing Securenet Payment...");

        if (!Validate(payment))
        {
            payment.Status = Enums.PaymentStatus.Failed;
            Console.WriteLine("Payment validation failed.");
            return;
        }

        payment.Status = Enums.PaymentStatus.Completed;

        Console.WriteLine("Securenet Payment Successful.");
    }

    public override void RefundPayment(Payment payment)
    {
        payment.Status = Enums.PaymentStatus.Refunded;

        Console.WriteLine("Securenet Payment Refunded Successfully.");
    }
}