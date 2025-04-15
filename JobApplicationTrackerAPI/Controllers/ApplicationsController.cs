using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAll()
        {
            var result = await _applicationService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDto>> GetById(int id)
        {
            var application = await _applicationService.GetByIdAsync(id);
            if (application == null) return NotFound();
            return Ok(application);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationDto>> Create(CreateApplicationRequest request)
        {
            var created = await _applicationService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateApplicationRequest request)
        {
            var success = await _applicationService.UpdateAsync(id, request);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _applicationService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
