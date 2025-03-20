using Ivan.DianPing.V2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ivan.DianPing.V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopAsync(long id)
        {
            var shop = await _shopService.GetShopByIdAsync(id);

            if (shop == null) { return NotFound(); }

            return Ok(shop);
        }
    }
}
