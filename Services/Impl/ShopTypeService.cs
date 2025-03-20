using AutoMapper;
using Ivan.DianPing.Repository;
using Ivan.DianPing.V2.Constants;
using Ivan.DianPing.V2.Models.PO;
using Ivan.DianPing.V2.Repository;

namespace Ivan.DianPing.V2.Services.Impl
{
    public class ShopTypeService : IShopTypeService
    {
        private RedisService _redisService;
        private ShopTypeRepository _shopTypeRepository;
        private IMapper _mapper;

        public ShopTypeService(ShopTypeRepository shopTypeRepository, IMapper mapper, RedisService redisService)
        {
            _redisService = redisService;
            _shopTypeRepository = shopTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShopType>> GetShopTypes()
        {
            var shopTypes = await _redisService.GetObjectFromStringAsync<IEnumerable<ShopType>>(CommonConstants.CACHE_SHOP_TYPES_KEY);

            // 1.商户类型缓存未命中 -> 查询商户类型（数据库查询）写入redis
            if (shopTypes is null)
            {
                shopTypes = await _shopTypeRepository.GetShopTypes();
                await _redisService.SetStringAsync(CommonConstants.CACHE_SHOP_TYPES_KEY, shopTypes, TimeSpan.FromMinutes(CommonConstants.CACHE_SHOP_TYPES_TTL));
            }

            return shopTypes;
        }
    }
}
