using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.Model;
using Mapster;
using MapsterMapper;
public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;
        private readonly IMapper _mapper;
        public CompanyService(ICompanyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(_mapper.Map<CompanyDto>);

        public async Task<CompanyDto?> GetByIdAsync(int id) =>
            (await _repo.GetByIdAsync(id))?.Let(_mapper.Map<CompanyDto>);

        public async Task<CompanyDto> CreateAsync(CompanyDto dto)
        {
            var model = _mapper.Map<Company>(dto);
            model.Id = await _repo.CreateAsync(model);
            return _mapper.Map<CompanyDto>(model);
        }

        public async Task<bool> UpdateAsync(int id, CompanyDto dto)
        {
            var model = _mapper.Map<Company>(dto);
            model.Id = id;
            return await _repo.UpdateAsync(model);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }