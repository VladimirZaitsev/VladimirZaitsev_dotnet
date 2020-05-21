using System.Threading.Tasks;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StudentApi.Controllers
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

        [HttpGet]
        public IActionResult Students()
        {
            var result = _studentService.GetAll();
            _logger.LogInformation("Fetched students");

            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Student(int id)
        {
            try
            {
                var user = await _studentService.GetByIdAsync(id);
                _logger.LogInformation("Searched for student");

                return Ok(user);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
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
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Updated([FromBody] Student student)
        {
            try
            {
               await _studentService.UpdateAsync(student);
                _logger.LogInformation("Added new student");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
