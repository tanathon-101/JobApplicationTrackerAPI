using JobApplicationTrackerAPI.DTOs.ApplicationDtos;
using JobApplicationTrackerAPI.Model;

namespace JobApplicationTrackerAPI.Repository
{
    public interface IApplicationRepository
    {
         Task<IEnumerable<ApplicationWithDetailsDto>> GetAllAsync();
        Task<ApplicationWithDetailsDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(Application application);
        Task<bool> UpdateAsync(Application application);
        Task<bool> DeleteAsync(int id);
    }
}