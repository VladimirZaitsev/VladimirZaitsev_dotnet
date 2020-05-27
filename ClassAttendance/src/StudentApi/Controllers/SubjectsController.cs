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
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<ClassesController> _logger;

        public SubjectsController(ISubjectService subjectService, ILogger<ClassesController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _subjectService.GetAll();
            _logger.LogInformation("Fetched classes");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _subjectService.GetByIdAsync(id);
                _logger.LogInformation("Searched for class");

                return Ok(user);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Subject subject)
        {
            try
            {
                var id = await _subjectService.AddAsync(subject);
                _logger.LogInformation("Added new class");

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
                await _subjectService.DeleteAsync(id);
                _logger.LogInformation("Deleted class");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Subject subject)
        {
            try
            {
                await _subjectService.UpdateAsync(subject);
                _logger.LogInformation("Added new class");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Students/{id}")]
        public IActionResult GetStudentsLearning(int id)
        {
            var result = _subjectService.GetStudentsAsync(id);

            return Ok(result);
        }

        [HttpGet("Students/{id}")]
        public IActionResult GetLecturersTeaching(int id)
        {
            var result = _subjectService.GetLecturersAsync(id);

            return Ok(result);
        }
    }
}
