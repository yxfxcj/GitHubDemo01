using Ivan.DianPing.V2.Models.PO;
using Ivan.DianPing.V2.Repository;

namespace Ivan.DianPing.V2.Services
{
    public interface IShopService
    {
        Task<Shop> GetShopByIdAsync(long id);
    }
}
