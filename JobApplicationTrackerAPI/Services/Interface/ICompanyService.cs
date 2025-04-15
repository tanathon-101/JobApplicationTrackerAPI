using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.DTOs.CompanyDtos;

public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<CompanyDto?> GetByIdAsync(int id);
        Task<CompanyDto> CreateAsync(CreateCompanyDto dto);
        Task<bool> UpdateAsync(int id, UpdateCompanyDto dto);
        Task<bool> DeleteAsync(int id);
    }