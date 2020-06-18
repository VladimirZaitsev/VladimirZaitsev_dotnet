using System.Threading.Tasks;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassAttendanceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ClassesController : Controller
    {
        private readonly IService<Class> _classService;
        private readonly ILogger<ClassesController> _logger;

        public ClassesController(IService<Class> classService, ILogger<ClassesController> logger)
        {
            _classService = classService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _classService.GetAll();
            _logger.LogInformation("Fetched classes");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _classService.GetByIdAsync(id);
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
        public async Task<IActionResult> Add([FromBody] Class item)
        {
            try
            {
                var id = await _classService.AddAsync(item);
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
                await _classService.DeleteAsync(id);
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
        public async Task<IActionResult> Update([FromBody] Class item)
        {
            try
            {
                await _classService.UpdateAsync(item);
                _logger.LogInformation("Added new class");

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
