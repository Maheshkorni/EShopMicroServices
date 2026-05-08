namespace Orders.Domain.ValueObjects
{
    public record Payment
    {
        private const int CVVLength = 3;
        public string? CardName { get; } = default;
        public string CardNumber { get; } = default!;
         public string Expiration {  get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        protected Payment() { }

        private Payment(string cardName,  string cardNumber, string expiration, string CVV, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = CVV;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardName, string cardNumber, string expiration, string CVV, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardName);
            ArgumentException.ThrowIfNullOrEmpty(cardNumber);
            ArgumentException.ThrowIfNullOrEmpty(expiration);
            ArgumentException.ThrowIfNullOrEmpty(CVV);
            ArgumentOutOfRangeException.ThrowIfNotEqual(CVV.Length, CVVLength);

            return new Payment(cardName, cardNumber, expiration, CVV, paymentMethod);

        }

    }
}
