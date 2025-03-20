using Ivan.DianPing.Models.DTO;
using Ivan.DianPing.V2.Models.PO;

namespace Ivan.DianPing.V2.Services
{
    public interface IShopTypeService
    {
        Task<IEnumerable<ShopType>> GetShopTypes();
    }
}
