using Ivan.DianPing.V2.Models.PO;
using Ivan.DianPing.V2.Repository;

namespace Ivan.DianPing.V2.Services
{
    public interface IShopService
    {
        /// <summary>
        /// /sdf dsfsdf
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Shop> GetShopByIdAsync(long id);
    }
}
