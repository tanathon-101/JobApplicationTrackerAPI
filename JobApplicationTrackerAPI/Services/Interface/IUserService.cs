using JobApplicationTrackerAPI.DTOs;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(UserRegisterDto dto);
    Task<bool> UpdateAsync(int id, UserUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ValidateCredentialsAsync(string email, string password);
    Task<string?> GenerateJwtTokenAsync(string email);
}