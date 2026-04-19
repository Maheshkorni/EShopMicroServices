namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellation = default)
        {
            session.Delete<ShoppingCart>(UserName);
            await session.SaveChangesAsync();
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(UserName);
            return basket is null ? throw new BasketNotFoundException(UserName) : basket;
        }

        public async Task<string> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            session.Store<ShoppingCart>(cart);
            await session.SaveChangesAsync();
            return cart.UserName;
        }
    }
}
