using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ClassAttendanceAPI.Controllers
{
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IGroupService groupService, ILogger<GroupsController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        [HttpGet("Groups")]
        public IActionResult GetAll()
        {
            var result = _groupService.GetAll();
            _logger.LogInformation("Fetched groups");

            return Ok(result);
        }

        [HttpGet("Group")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var user = await _groupService.GetByIdAsync(id);
                _logger.LogInformation("Searched for Group");

                return Ok(user);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Group")]
        public async Task<IActionResult> Add([FromBody] Group group)
        {
            try
            {
                var id = await _groupService.AddAsync(group);
                _logger.LogInformation("Added new class");

                return Ok(id);
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Group")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _groupService.DeleteAsync(id);
                _logger.LogInformation("Deleted class");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Group")]
        public async Task<IActionResult> Update([FromBody] Group group)
        {
            try
            {
                await _groupService.UpdateAsync(group);
                _logger.LogInformation("Added new class");

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudentsAsync(int groupId)
        {
            var students = await _groupService.GetAllStudentsAsync(groupId);

            return Ok(students);
        }
    }
}
