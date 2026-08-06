using Interfaces_Abstraction.Enums;
using Interfaces_Abstraction.Helpers;
using Interfaces_Abstraction.Models;
using Interfaces_Abstraction.Services;

namespace Interfaces_Abstraction.UI;

public class ConsoleUI
{
    private readonly PaymentService _paymentService;

    public ConsoleUI()
    {
        _paymentService = new PaymentService();
    }

    public void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("====================================");
            Console.WriteLine("     Payment Management System");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Process Payment");
            Console.WriteLine("2. Refund Payment");
            Console.WriteLine("3. View Processed Payments");
            Console.WriteLine("4. Exit");
            Console.WriteLine();

            int choice = ConsoleHelper.ReadInt("Select an option: ");

            switch (choice)
            {
                case 1:
                    ProcessPayment();
                    break;
                case 2:
                    RefundPayment();
                    break;
                case 3:
                    ViewProcessedPayments();
                    break;
                case 4:
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private void ProcessPayment()
    {
        Console.Clear();

        Console.WriteLine("===== Process Payment =====");
        Console.WriteLine();

        int paymentId = ConsoleHelper.ReadInt("Enter Payment ID: ");
        decimal amount = ConsoleHelper.ReadDecimal("Enter Amount: ");

        Console.WriteLine();
        Console.WriteLine("Select Payment Method:");
        Console.WriteLine("1. Stripe");
        Console.WriteLine("2. Securenet");
        Console.WriteLine("3. Paypal");

        PaymentMethod paymentMethod;

        while (true)
        {
            int method = ConsoleHelper.ReadInt("Enter your choice: ");

            if (Enum.IsDefined(typeof(PaymentMethod), method))
            {
                paymentMethod = (PaymentMethod)method;
                break;
            }

            Console.WriteLine("Invalid payment method. Please try again.");
        }

        Payment payment = new()
        {
            PaymentId = paymentId,
            Amount = amount,
            PaymentDate = DateTime.Now,
            PaymentMethod = paymentMethod,
            Status = PaymentStatus.Pending
        };

        _paymentService.ProcessPayment(payment);

        Console.WriteLine();
        Console.WriteLine($"Payment Status : {payment.Status}");

        ConsoleHelper.Pause();
    }

    private void RefundPayment()
    {
        Console.Clear();

        Console.WriteLine("===== Refund Payment =====");
        Console.WriteLine();

        int paymentId = ConsoleHelper.ReadInt("Enter Payment ID: ");

        // Look up the ORIGINAL processed payment instead of creating a new
        // blank one — this is what preserves StripePaymentIntentId.
        Payment? payment = _paymentService.GetPaymentById(paymentId);

        if (payment == null)
        {
            Console.WriteLine();
            Console.WriteLine($"No processed payment found with ID {paymentId}.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Found Payment  : ID {payment.PaymentId}, Amount {payment.Amount}, Method {payment.PaymentMethod}, Status {payment.Status}");

        _paymentService.RefundPayment(payment.PaymentId);

        Console.WriteLine();
        Console.WriteLine($"Payment Status: {payment.Status}");

        ConsoleHelper.Pause();
    }

    private void ViewProcessedPayments()
    {
        Console.Clear();

        Console.WriteLine("===== Processed Payments =====");
        Console.WriteLine();

        var payments = _paymentService.GetAllPayments();

        if (payments.Count == 0)
        {
            Console.WriteLine("No payments have been processed yet.");
        }
        else
        {
            foreach (var payment in payments)
            {
                Console.WriteLine($"ID: {payment.PaymentId} | Amount: {payment.Amount} | Method: {payment.PaymentMethod} | Status: {payment.Status} | Date: {payment.PaymentDate} | StripePaymentIntentId: {payment.StripePaymentIntentId}");
            }
        }

        ConsoleHelper.Pause();
    }
}