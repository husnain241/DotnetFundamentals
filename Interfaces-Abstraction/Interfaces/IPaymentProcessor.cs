using Interfaces_Abstraction.Models;

namespace Interfaces_Abstraction.Interfaces;

public interface IPaymentProcessor
{
	bool Validate(Payment payment);

	void ProcessPayment(Payment payment);

	void RefundPayment(Payment payment);
}