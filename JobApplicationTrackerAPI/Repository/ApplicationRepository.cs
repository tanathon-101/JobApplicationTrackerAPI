using JobApplicationTrackerAPI.Model;
using Dapper;

using System.Data;
using Microsoft.Data.SqlClient;
using JobApplicationTrackerAPI.DTOs.ApplicationDtos;

namespace JobApplicationTrackerAPI.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly string _connectionString;

        public ApplicationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("JobTrackerDb")!;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<ApplicationWithDetailsDto>> GetAllAsync()
        {
            const string sql = @"
        SELECT 
            a.Id,
            a.UserId,
            a.CompanyId,
            c.Name AS CompanyName,
            a.PositionId,
            p.Title AS PositionTitle,
            a.Status,
            a.AppliedDate,
            a.InterviewDate,
            a.Notes
        FROM Applications a
        JOIN Companies c ON a.CompanyId = c.Id
        JOIN Positions p ON a.PositionId = p.Id";
            using var conn = CreateConnection();
            return await conn.QueryAsync<ApplicationWithDetailsDto>(sql);
        }

        public async Task<ApplicationWithDetailsDto?> GetByIdAsync(int id)
        {
            const string sql = @"
        SELECT 
            a.Id,
            a.UserId,
            a.CompanyId,
            c.Name AS CompanyName,
            a.PositionId,
            p.Title AS PositionTitle,
            a.Status,
            a.AppliedDate,
            a.InterviewDate,
            a.Notes
        FROM Applications a
        JOIN Companies c ON a.CompanyId = c.Id
        JOIN Positions p ON a.PositionId = p.Id
        WHERE Id = @Id ";

            using var conn = CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<ApplicationWithDetailsDto>(sql, new { Id = id });
        }

        public async Task<int> CreateAsync(Application application)
        {
            const string sql = @"
                INSERT INTO Applications (UserId, CompanyId, PositionId, Status, AppliedDate, InterviewDate, Notes)
                VALUES (@UserId, @CompanyId, @PositionId, @Status, @AppliedDate, @InterviewDate, @Notes);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            using var conn = CreateConnection();
            return await conn.ExecuteScalarAsync<int>(sql, application);
        }

        public async Task<bool> UpdateAsync(Application application)
        {
            const string sql = @"
                UPDATE Applications
                SET CompanyId = @CompanyId,
                    PositionId = @PositionId,
                    Status = @Status,
                    AppliedDate = @AppliedDate,
                    InterviewDate = @InterviewDate,
                    Notes = @Notes
                WHERE Id = @Id";

            using var conn = CreateConnection();
            var rows = await conn.ExecuteAsync(sql, application);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Applications WHERE Id = @Id";
            using var conn = CreateConnection();
            var rows = await conn.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}