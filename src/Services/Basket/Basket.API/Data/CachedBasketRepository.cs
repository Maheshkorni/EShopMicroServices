using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository,IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellation = default)
        {
           var result =  await repository.DeleteBasket(UserName, cancellation);
            if (result)
                await cache.RemoveAsync(UserName, cancellation);
            return result;
        }

        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(UserName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            var basket = await repository.GetBasket(UserName, cancellationToken);
            await cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket));
            return basket;
        }

        public async Task<string> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            var userName =  await repository.StoreBasket(cart, cancellationToken);
            if(!string.IsNullOrEmpty(userName))
                await cache.SetStringAsync(userName, JsonSerializer.Serialize(cart));
            return userName;
        }
    }
}
