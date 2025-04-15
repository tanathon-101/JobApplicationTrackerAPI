using JobApplicationTrackerAPI.Model;

public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetAllAsync();
        Task<Position?> GetByIdAsync(int id);
        Task<int> CreateAsync(Position position);
        Task<bool> UpdateAsync(Position position);
        Task<bool> DeleteAsync(int id);
    }