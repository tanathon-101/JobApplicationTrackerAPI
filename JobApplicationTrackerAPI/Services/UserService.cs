using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.Model;
using MapsterMapper;

namespace JobApplicationTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(_mapper.Map<UserDto>);

        public async Task<UserDto?> GetByIdAsync(int id) =>
            (await _repo.GetByIdAsync(id))?.Let(_mapper.Map<UserDto>);

        public async Task<UserDto> CreateAsync(UserDto dto)
        {
            var model = _mapper.Map<User>(dto);
            model.Id = await _repo.CreateAsync(model);
            return _mapper.Map<UserDto>(model);
        }

        public async Task<bool> UpdateAsync(int id, UserDto dto)
        {
            var model = _mapper.Map<User>(dto);
            model.Id = id;
            return await _repo.UpdateAsync(model);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}