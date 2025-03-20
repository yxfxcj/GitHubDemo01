using Dapper;
using Ivan.DianPing.V2.Models.PO;
using System.Data;

namespace Ivan.DianPing.V2.Repository
{
    public class ShopTypeRepository
    {
        private readonly IDbConnection _connection;

        public ShopTypeRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<ShopType>> GetShopTypes()
        {
            string sql = @"SELECT [Id]
                                 ,[Name]
                                 ,[Icon]
                                 ,[Sort]
                          FROM [DianPing].[dbo].[ShopType]";
            return await _connection.QueryAsync<ShopType>(sql);
        }
    }
}
