using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return data is null ? NotFound() : Ok(data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto) => Ok(await _service.CreateAsync(dto));

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto dto)
        {
            // Simplified demo auth (no token)
            return dto.Email == "test@example.com" && dto.PasswordHash == "1234"
                ? Ok("Login success")
                : Unauthorized("Invalid credentials");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserDto dto) =>
            await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}