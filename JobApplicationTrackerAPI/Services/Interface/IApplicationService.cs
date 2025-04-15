using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.DTOs.ApplicationDtos;

namespace JobApplicationTrackerAPI.Services.Interface
{
    public interface IApplicationService
    {
         Task<IEnumerable<ApplicationWithDetailsDto>> GetAllAsync();
         Task<ApplicationWithDetailsDto?> GetByIdAsync(int id);
         Task<ApplicationDto> CreateAsync(CreateApplicationRequest request);
         Task<bool> UpdateAsync(int id, UpdateApplicationRequest request);
          Task<bool> DeleteAsync(int id);
    }
}