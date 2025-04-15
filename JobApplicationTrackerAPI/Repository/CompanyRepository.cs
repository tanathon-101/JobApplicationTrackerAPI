using System.Data;
using Dapper;
using JobApplicationTrackerAPI.Model;
using Microsoft.Data.SqlClient;


namespace JobApplicationTrackerAPI.Repository
{
    public class CompanyRepository :ICompanyRepository
    {
         private readonly string _connStr;
        public CompanyRepository(IConfiguration config) => _connStr = config.GetConnectionString("JobTrackerDb")!;
        private IDbConnection Conn => new SqlConnection(_connStr);

        public async Task<IEnumerable<Company>> GetAllAsync() =>
            await Conn.QueryAsync<Company>("SELECT * FROM Companies");

        public async Task<Company?> GetByIdAsync(int id) =>
            await Conn.QueryFirstOrDefaultAsync<Company>("SELECT * FROM Companies WHERE Id = @id", new { id });

        public async Task<int> CreateAsync(Company c) =>
            await Conn.ExecuteScalarAsync<int>("INSERT INTO Companies (Name, Website) VALUES (@Name, @Website); SELECT SCOPE_IDENTITY();", c);

        public async Task<bool> UpdateAsync(Company c) =>
            await Conn.ExecuteAsync("UPDATE Companies SET Name = @Name, Website = @Website WHERE Id = @Id", c) > 0;

        public async Task<bool> DeleteAsync(int id) =>
            await Conn.ExecuteAsync("DELETE FROM Companies WHERE Id = @id", new { id }) > 0;
    }
}