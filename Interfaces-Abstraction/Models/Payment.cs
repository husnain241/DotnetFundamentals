using Interfaces_Abstraction.Enums;

namespace Interfaces_Abstraction.Models;

public class Payment
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? StripePaymentIntentId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public PaymentStatus Status { get; set; }

}