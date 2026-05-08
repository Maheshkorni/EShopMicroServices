namespace Orders.Domain.ValueObjects
{
    public record OrderName
    {
        public string value { get; }
        private const int DefaultLength = 5;

        private OrderName(string name) => value = name; 
        
        public static OrderName Of(string name) 
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, DefaultLength);
            return new OrderName(name);
        }
    }
}
