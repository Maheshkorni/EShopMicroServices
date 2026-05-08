namespace Orders.Domain.ValueObjects
{
    public record ProductId
    {
        public Guid value { get; }

        private ProductId(Guid value) => this.value = value;

        public static ProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
                throw new DomainException("ProductId cannot be Empty");

            return new ProductId(value);
        }
    }
}
