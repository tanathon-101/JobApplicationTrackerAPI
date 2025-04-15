using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.DTOs.PositionDtos;
using JobApplicationTrackerAPI.Model;
using MapsterMapper;

namespace JobApplicationTrackerAPI.Services
{
 public class PositionService : IPositionService
    {
        private readonly IPositionRepository _repo;
        private readonly IMapper _mapper;
        public PositionService(IPositionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PositionDto>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(_mapper.Map<PositionDto>);

        public async Task<PositionDto?> GetByIdAsync(int id) =>
            (await _repo.GetByIdAsync(id))?.Let(_mapper.Map<PositionDto>);

        public async Task<PositionDto> CreateAsync(CreatePositionDto dto)
        {
            var model = _mapper.Map<Position>(dto);
            model.Id = await _repo.CreateAsync(model);
            return _mapper.Map<PositionDto>(model);
        }

        public async Task<bool> UpdateAsync(int id, UpdatePositionDto dto)
        {
            var model = _mapper.Map<Position>(dto);
            model.Id = id;
            return await _repo.UpdateAsync(model);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}