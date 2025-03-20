using AutoMapper;
using Ivan.DianPing.V2.Constants;
using Ivan.DianPing.V2.Models.PO;
using Ivan.DianPing.V2.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Ivan.DianPing.V2.Services.Impl
{
    public class ShopService : IShopService
    {
        private RedisService _redisService;
        private ShopRepository _shopRepository;

        public ShopService(ShopRepository shopRepository, RedisService redisService)
        {
            _redisService = redisService;
            _shopRepository = shopRepository;
        }

        public async Task<Shop> GetShopByIdAsync(long id)
        {
            // 1.判断缓存是否命中
            var key = string.Format(CommonConstants.CACHE_SHOP_KEY_PREFIX, id);
            var shopJson = await _redisService.GetStringAsync(key);

            // 2.命中空缓存 返回null - 缓存穿透处理
            if (shopJson == CommonConstants.CACHE_SHOP_NULL_DEFAULT) return default;

            // 2.命中 返回商品
            if (!string.IsNullOrEmpty(shopJson)) 
            {
                return JsonSerializer.Deserialize<Shop>(shopJson);
            }

            // 4.查询数据库
            var shop = await _shopRepository.GetShopById(id);

            // 5.数据库不存在，返回null 并且将空字符串写了缓存 防止缓存穿透
            if (shop == null) { await _redisService.SetStringAsync(key, CommonConstants.CACHE_SHOP_NULL_DEFAULT); }

            // 6.存在写入redis
            await _redisService.SetStringAsync(key, JsonSerializer.Serialize(shop), TimeSpan.FromMinutes(CommonConstants.CACHE_SHOP_KEY_EMPTY_TTL));

            return shop;
        }
    }
}
