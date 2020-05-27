using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ClassAttendanceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class LecturersController : Controller
    {
        private readonly IService<Lecturer> _lecturerService;
        private readonly ILogger<LecturersController> _logger;

        public LecturersController(IService<Lecturer> lecturerService, ILogger<LecturersController> logger)
        {
            _lecturerService = lecturerService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _lecturerService.GetAll();
            _logger.LogInformation("Fetched lecturers");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _lecturerService.GetByIdAsync(id);
                _logger.LogInformation("Searched for lecturer");

                return Ok(user);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Lecturer lecturer)
        {
            try
            {
                var id = await _lecturerService.AddAsync(lecturer);
                _logger.LogInformation("Added new lecturer");

                return Ok(id);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _lecturerService.DeleteAsync(id);
                _logger.LogInformation("Deleted lecturer");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Lecturer lecturer)
        {
            try
            {
                await _lecturerService.UpdateAsync(lecturer);
                _logger.LogInformation("Added new student");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
