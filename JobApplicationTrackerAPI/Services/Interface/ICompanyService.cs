using JobApplicationTrackerAPI.DTOs;

public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<CompanyDto?> GetByIdAsync(int id);
        Task<CompanyDto> CreateAsync(CompanyDto dto);
        Task<bool> UpdateAsync(int id, CompanyDto dto);
        Task<bool> DeleteAsync(int id);
    }