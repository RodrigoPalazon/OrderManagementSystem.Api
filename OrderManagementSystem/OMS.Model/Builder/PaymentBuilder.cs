namespace OMS.Model
{
    public class PaymentBuilder
    {
        private Payment _payment; // Why is this field readonly if I'm modifying its properties? 

        public PaymentBuilder()
        {
            _payment = new Payment
            {
                // OrderId = _orderNumberingService.GetNewOrderId(),
                PaymentDate = DateTime.Now,
                Status = PaymentStatus.Pending
            };
        }

        //public PaymentBuilder ForOrder(int orderId)
        //{
        //    _payment.OrderId = orderId;
        //    return this; // Is 'this' referring to the current instance of PaymentBuilder? Why this allow method chaining?
        //}

        public PaymentBuilder WithAmount(decimal amount)
        {
            _payment.Amount = amount;

            // add more complex logic if needed
            _payment.isDebit = amount < 0;

            return this;
        }

        public PaymentBuilder WithPaymentMethod(PaymentMethod paymentMethod)
        {
            _payment.PaymentMethod = paymentMethod;
            return this;
        }

        public PaymentBuilder WithPaymentDate(DateTime paymentDate)
        {
            _payment.PaymentDate = paymentDate;
            return this;
        }

        public PaymentBuilder WithStatus(PaymentStatus status)
        {
            _payment.Status = status;
            return this;
        }

        public PaymentBuilder WithTransactionReference(string transactionReference)
        {
            _payment.TransactionReference = transactionReference ?? string.Empty;
            return this;
        }

        public PaymentBuilder ForOrder(int orderId)
        {
            _payment.OrderId = orderId;
            return this;
        }

        public PaymentBuilder WithNotes(string notes)
        {
            _payment.Notes = notes ?? string.Empty;
            return this;
        }

        public Payment Build() // Apart from the Build method, what other methods are commonly found in builder classes? for instance Reset(), Validate()....
        {
            if (_payment.OrderId <= 0)
                throw new InvalidOperationException("OrderId is required.");

            if (_payment.Amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero.");

            return _payment;
        }
    }
}

//What are the most common uses of the Builder pattern in C#? 

//General question: I started develolping this Payment flow from Model, Builder, IRepository, Repository, update OMSDbContext, IPaymentService, PaymentService, Controller, Dependency Injection, and finally DB Migration (still to be fixed). Is this the correct order of development? If not, what would be a better order?