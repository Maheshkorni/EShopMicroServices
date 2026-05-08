namespace Orders.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid value { get; }

        private OrderItemId(Guid value) => this.value = value;

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if(value == Guid.Empty)
            {
                throw new DomainException("OrderItem Id cannot be empty.");
            }
            return new OrderItemId(value);
        }
    }
}
