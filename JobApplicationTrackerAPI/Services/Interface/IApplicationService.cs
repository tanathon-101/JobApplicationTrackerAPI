using JobApplicationTrackerAPI.DTOs;

namespace JobApplicationTrackerAPI.Services.Interface
{
    public interface IApplicationService
    {
         Task<IEnumerable<ApplicationDto>> GetAllAsync();
         Task<ApplicationDto?> GetByIdAsync(int id);
         Task<ApplicationDto> CreateAsync(CreateApplicationRequest request);
         Task<bool> UpdateAsync(int id, UpdateApplicationRequest request);
          Task<bool> DeleteAsync(int id);
    }
}