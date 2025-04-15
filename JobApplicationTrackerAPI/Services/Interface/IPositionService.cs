using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.DTOs.PositionDtos;

public interface IPositionService
    {
        Task<IEnumerable<PositionDto>> GetAllAsync();
        Task<PositionDto?> GetByIdAsync(int id);
        Task<PositionDto> CreateAsync(CreatePositionDto dto);
        Task<bool> UpdateAsync(int id, UpdatePositionDto dto);
        Task<bool> DeleteAsync(int id);
    }