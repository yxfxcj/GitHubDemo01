using Dapper;
using Ivan.DianPing.V2.Models.PO;
using System.Data;

namespace Ivan.DianPing.V2.Repository
{
    public class ShopRepository
    {
        private readonly IDbConnection _connection;

        public ShopRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Shop> GetShopById(long id)
        {
            string sql = @"SELECT  [Id]
                                  ,[Name]
                                  ,[TypeId]
                                  ,[Images]
                                  ,[Area]
                                  ,[Address]
                                  ,[X]
                                  ,[Y]
                                  ,[AvgPrice]
                                  ,[Sold]
                                  ,[Comments]
                                  ,[Score]
                                  ,[OpenHours]
                                  ,[CreateTime]
                                  ,[UpdateTime]
                              FROM [DianPing].[dbo].[Shop] WHERE [Id] = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Shop>(sql, new { Id = id });
        }


    }
}
