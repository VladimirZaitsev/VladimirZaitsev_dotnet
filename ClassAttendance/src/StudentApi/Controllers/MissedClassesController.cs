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
    public class MissedClassesController : Controller
    {
        private readonly IMissedClassService _missedClassService;
        private readonly ILogger<MissedClassesController> _logger;

        public MissedClassesController(IMissedClassService missedClassService, ILogger<MissedClassesController> logger)
        {
            _missedClassService = missedClassService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _missedClassService.GetAll();
            _logger.LogInformation("Fetched missed classes");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _missedClassService.GetByIdAsync(id);
                _logger.LogInformation("Searched for missed class");

                return Ok(user);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MissedClass missedClass)
        {
            try
            {
                var id = await _missedClassService.AddAsync(missedClass);
                _logger.LogInformation("Added new missed class");

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
                await _missedClassService.DeleteAsync(id);
                _logger.LogInformation("Deleted missed class");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MissedClass missedClass)
        {
            try
            {
                await _missedClassService.UpdateAsync(missedClass);
                _logger.LogInformation("Added new class");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Slackers")]
        public async Task<IActionResult> GetAllStudentsAsync(Class classModel)
        {
            var students = await _missedClassService.GetSlackersAsync(classModel);

            return Ok(students);
        }

        [HttpGet("Student/{id}")]
        public async Task<IActionResult> GetStudentsMissedLectures(int id)
        {
            var result = await _missedClassService.GetMissedLecturesByStudentAsync(id);

            return Ok(result);
        }

        [HttpGet("Lecturer/{id}")]
        public async Task<IActionResult> GetLecturersMissedLectures(int id)
        {
            var result = await _missedClassService.GetMissedLecturesByLecturerAsync(id);

            return Ok(result);
        }
    }
}
