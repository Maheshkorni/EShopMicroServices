namespace Orders.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid value { get; }
        private OrderId(Guid value) => value = value;

        public static OrderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if(value == Guid.Empty)
            {
                throw new DomainException("OrderId cannot be empty");
            }

            return new OrderId(value);
        }
    }
}
