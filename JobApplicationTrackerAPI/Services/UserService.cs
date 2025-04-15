// UserService.cs (with BCrypt & JWT)
using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.Model;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace JobApplicationTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repo, IMapper mapper, IConfiguration config)
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(_mapper.Map<UserDto>);

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> CreateAsync(UserRegisterDto dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hashedPassword
            };

            user.Id = await _repo.CreateAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user is null) return false;

            user.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            return await _repo.UpdateAsync(user);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _repo.GetByEmailAsync(email);
            return user is not null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public async Task<string?> GenerateJwtTokenAsync(string email)
        {
            var user = await _repo.GetByEmailAsync(email);
            if (user is null) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
