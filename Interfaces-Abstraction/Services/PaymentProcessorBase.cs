using Interfaces_Abstraction.Interfaces;
using Interfaces_Abstraction.Models;

namespace Interfaces_Abstraction.Services;

public abstract class PaymentProcessorBase : IPaymentProcessor
{
	public virtual bool Validate(Payment payment)
	{
		return payment.Amount > 0;
	}

	public abstract void ProcessPayment(Payment payment);

	public abstract void RefundPayment(Payment payment);
}