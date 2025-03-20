using Dapper;
using Ivan.DianPing.Models.PO;
using Ivan.DianPing.Utils;
using System.Data;

namespace Ivan.DianPing.Repository
{
    public class UserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<long> CreateUserAsync(string phone)
        {
            string sql = @"
            INSERT INTO [User] (Phone, Nickname) 
            VALUES (@Phone, @Nickname);
            SELECT CAST(SCOPE_IDENTITY() as BIGINT)";

            return await _connection.ExecuteScalarAsync<long>(sql, new { Phone = phone, Nickname = CommonUtil.GenerateUserNickname() });
        }

        public async Task<User> GetByIdAsync(long id)
        {
            string sql = @"SELECT Id, Phone, Password, Nickname, Icon FROM [User] WHERE Id = @Id";
            User? user = await _connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
            return user;
        }

        public Task<User?> GetByPhoneAsync(string phone)
        {
            string sql = @"SELECT Id, Phone, Password, Nickname, Icon FROM [User] WHERE Phone = @Phone";
            return _connection.QueryFirstOrDefaultAsync<User>(sql, new { Phone = phone });
        }
    }
}
