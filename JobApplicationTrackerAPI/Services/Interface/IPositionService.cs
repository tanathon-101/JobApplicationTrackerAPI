using JobApplicationTrackerAPI.DTOs;

public interface IPositionService
    {
        Task<IEnumerable<PositionDto>> GetAllAsync();
        Task<PositionDto?> GetByIdAsync(int id);
        Task<PositionDto> CreateAsync(PositionDto dto);
        Task<bool> UpdateAsync(int id, PositionDto dto);
        Task<bool> DeleteAsync(int id);
    }