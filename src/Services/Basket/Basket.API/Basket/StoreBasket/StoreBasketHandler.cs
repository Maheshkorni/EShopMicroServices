using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotNull().WithMessage("UserName is required");
        }
    }

    public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient coupon) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;
            foreach (ShoppingCartItem cartItem in cart.Items)
            {
                var discount = await coupon.GetDiscountAsync(new GetDiscountRequest() { ProductName = cartItem.ProductName });
                if (discount != null && discount.Amount > 0 )
                {
                    cartItem.Price -= discount.Amount;
                }
            }
            var result = await repository.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(result);
        }
    }
}
