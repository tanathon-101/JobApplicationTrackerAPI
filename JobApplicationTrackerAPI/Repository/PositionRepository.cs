
using System.Data;
using Dapper;
using JobApplicationTrackerAPI.Model;
using Microsoft.Data.SqlClient;
public class PositionRepository : IPositionRepository
{
    private readonly string _connStr;
    public PositionRepository(IConfiguration config) => _connStr = config.GetConnectionString("JobTrackerDb")!;
    private IDbConnection Conn => new SqlConnection(_connStr);

    public async Task<IEnumerable<Position>> GetAllAsync() =>
        await Conn.QueryAsync<Position>("SELECT * FROM Positions");

    public async Task<Position?> GetByIdAsync(int id) =>
        await Conn.QueryFirstOrDefaultAsync<Position>("SELECT * FROM Positions WHERE Id = @id", new { id });

    public async Task<int> CreateAsync(Position p) =>
        await Conn.ExecuteScalarAsync<int>("INSERT INTO Positions (Title) VALUES (@Title); SELECT SCOPE_IDENTITY();", p);

    public async Task<bool> UpdateAsync(Position p) =>
        await Conn.ExecuteAsync("UPDATE Positions SET Title = @Title WHERE Id = @Id", p) > 0;

    public async Task<bool> DeleteAsync(int id) =>
        await Conn.ExecuteAsync("DELETE FROM Positions WHERE Id = @id", new { id }) > 0;
}