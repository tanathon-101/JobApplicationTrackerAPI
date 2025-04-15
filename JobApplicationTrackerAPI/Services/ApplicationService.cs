using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.DTOs.ApplicationDtos;
using JobApplicationTrackerAPI.Model;
using JobApplicationTrackerAPI.Repository;
using JobApplicationTrackerAPI.Services.Interface;
using Mapster;

namespace JobApplicationTrackerAPI.Services
{

    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;

        public ApplicationService(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ApplicationWithDetailsDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ApplicationWithDetailsDto?> GetByIdAsync(int id)
        {
          return await _repository.GetByIdAsync(id);
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