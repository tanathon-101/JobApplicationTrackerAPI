using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.Model;
using JobApplicationTrackerAPI.Repository;
using JobApplicationTrackerAPI.Services.Interface;

namespace JobApplicationTrackerAPI.Services
{

    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;

        public ApplicationService(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ApplicationDto>> GetAllAsync()
        {
            var apps = await _repository.GetAllAsync();
            return apps.Adapt<IEnumerable<ApplicationDto>>();
        }

        public async Task<ApplicationDto?> GetByIdAsync(int id)
        {
            var app = await _repository.GetByIdAsync(id);
            return app?.Adapt<ApplicationDto>();
        }

        public async Task<ApplicationDto> CreateAsync(CreateApplicationRequest request)
        {
            var model = request.Adapt<Application>();
            model.Status = ApplicationStatus.Pending;
            var id = await _repository.CreateAsync(model);
            model.Id = id;
            return model.Adapt<ApplicationDto>();
        }

        public async Task<bool> UpdateAsync(int id, UpdateApplicationRequest request)
        {
            var model = request.Adapt<Application>();
            model.Id = id;
            return await _repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}