using JobApplicationTrackerAPI.DTOs;
using JobApplicationTrackerAPI.Services;
using JobApplicationTrackerAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {

        private readonly IRabbitMQPublisherService _publisher;
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService, IRabbitMQPublisherService publisher)
        {
            _publisher = publisher;
            _applicationService = applicationService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> Apply([FromBody] JobApplicationRequest request)
        {
          await  _publisher.PublishAsync(request, "job_application");

            return Ok(new { message = "Applied successfully and message sent!" });
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
