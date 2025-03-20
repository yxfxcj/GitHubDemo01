using Ivan.DianPing.V2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ivan.DianPing.V2.Controllers
{
    [Route("api/shop-type")]
    [ApiController]
    public class ShopTypeController : ControllerBase
    {
        private IShopTypeService _shopTypeService;

        public ShopTypeController(IShopTypeService shopTypeService)
        {
            _shopTypeService = shopTypeService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetShopTypes()
        {
            var shopTypes = await _shopTypeService.GetShopTypes();

            return Ok(shopTypes);
        }
    }
}
