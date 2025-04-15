using System.Data;
using Dapper;
using JobApplicationTrackerAPI.Model;
using Microsoft.Data.SqlClient;
namespace JobApplicationTrackerAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connStr;
        public UserRepository(IConfiguration config) => _connStr = config.GetConnectionString("JobTrackerDb")!;
        private IDbConnection Conn => new SqlConnection(_connStr);

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await Conn.QueryAsync<User>("SELECT * FROM Users");

        public async Task<User?> GetByIdAsync(int id) =>
            await Conn.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @id", new { id });

        public async Task<int> CreateAsync(User u) =>
            await Conn.ExecuteScalarAsync<int>("INSERT INTO Users (Email, PasswordHash) VALUES (@Email, @PasswordHash); SELECT SCOPE_IDENTITY();", u);

        public async Task<bool> UpdateAsync(User u) =>
            await Conn.ExecuteAsync("UPDATE Users SET Email = @Email, PasswordHash = @PasswordHash WHERE Id = @Id", u) > 0;

        public async Task<bool> DeleteAsync(int id) =>
            await Conn.ExecuteAsync("DELETE FROM Users WHERE Id = @id", new { id }) > 0;
        public async Task<User?> GetByEmailAsync(string email) =>
        await Conn.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @email", new { email });

    }
}