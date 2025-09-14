using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_AdvanceLearning
{
    public class Payment
    {
        public decimal Amount { get; set; }
    }

    public class CreditCardPayment : Payment
    {
        public string CardNumber { get; set; }
    }

    public class PayPalPayment : Payment
    {
        public string PayPalEmail { get; set; }
    }

    public interface IPaymentProcessor<in T> where T : Payment
    {
        void Process(T payment);
    }

    public class CreditCardPaymentProcessor : IPaymentProcessor<CreditCardPayment>
    {
        public void Process(CreditCardPayment payment)
        {
            Console.WriteLine($"Processing Credit Card Payment of {payment.Amount:C} using card {payment.CardNumber}");
        }
    }

    public class PayPalPaymentProcessor : IPaymentProcessor<PayPalPayment>
    {
        public void Process(PayPalPayment payment)
        {
            Console.WriteLine($"Processing PayPal Payment of {payment.Amount:C} for email {payment.PayPalEmail}");
        }
    }

    public class PaymentSelector
    {
        private readonly Dictionary<string, Action> _paymentActions;

        public PaymentSelector()
        {
            _paymentActions = new Dictionary<string, Action>
            {
                { "creditcard", ProcessCreditCardPayment },
                { "paypal", ProcessPayPalPayment }
            };
        }

        public void SelectAndProcess(string method)
        {
            if (_paymentActions.TryGetValue(method.ToLower(), out var action))
            {
                action();
            }
            else
            {
                Console.WriteLine("Invalid payment method selected.");
            }
        }

        private void ProcessCreditCardPayment()
        {
            CreditCardPayment payment = new CreditCardPayment
            {
                Amount = 150,
                CardNumber = "1234-5678-9999"
            };

            IPaymentProcessor<CreditCardPayment> processor = new CreditCardPaymentProcessor();
            processor.Process(payment);
        }

        private void ProcessPayPalPayment()
        {
            PayPalPayment payment = new PayPalPayment
            {
                Amount = 75,
                PayPalEmail = "user@example.com"
            };

            IPaymentProcessor<PayPalPayment> processor = new PayPalPaymentProcessor();
            processor.Process(payment);
        }
    }

    public class MultiplePaymentProcessor
    {
        private readonly PaymentSelector _paymentSelector;
        public MultiplePaymentProcessor()
        {
            _paymentSelector = new PaymentSelector();
        }

        public void ProcessPayments(string method)
        {
            string paymentMethod = method.ToLower();
            if (paymentMethod == "creditcard" || paymentMethod == "paypal")
            {
                _paymentSelector.SelectAndProcess(paymentMethod);
            }
            else
            {
                Console.WriteLine("Invalid payment method selected.");
            }

        }
    }
}
