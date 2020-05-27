using System.Threading.Tasks;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassAttendanceAPI.Controllers
{
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet("Students")]
        public IActionResult GetAll()
        {
            var result = _studentService.GetAll();
            _logger.LogInformation("Fetched students");

            return Ok(result);
        }

        [HttpGet("Student")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _studentService.GetByIdAsync(id);
                _logger.LogInformation("Searched for student");

                return Ok(user);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Student")]
        public async Task<IActionResult> Add([FromBody] Student student)
        {
            try
            {
                var id = await _studentService.AddAsync(student);
                _logger.LogInformation("Added new student");

                return Ok(id);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Student")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentService.DeleteAsync(id);
                _logger.LogInformation("Deleted student");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Student")]
        public async Task<IActionResult> Update([FromBody] Student student)
        {
            try
            {
                await _studentService.UpdateAsync(student);
                _logger.LogInformation("Added new student");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("StudentGroup")]
        public async Task<IActionResult> StudentGroup(int id)
        {
            try
            {
                var group = await _studentService.GetStudentGroupAsync(id);
                _logger.LogInformation("Fetched student group");

                return Ok(group);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
