using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassAttendanceAPI.Controllers
{
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            try
            {
                return Ok(await _identityService.GetByIdAsync(id));
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsersAsync() => Ok(await _identityService.GetUsersAsync());

        [HttpPut("User")]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            try
            {
                await _identityService.UpdateAsync(user);

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("User")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _identityService.DeleteAsync(id);

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("User")]
        public async Task<IActionResult> RegisterAsync(User user, string password)
        {
            try
            {
                await _identityService.RegisterAsync(user, password);

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                await _identityService.SignOutAsync();

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(string email, string password)
        {
            try
            {
                await _identityService.SignInAsync(email, password);

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> FindByEmailAsync(string email)
        {
            try
            {
                return Ok(await _identityService.FindByEmailAsync(email));
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Role")]
        public async Task<IActionResult> CreateRoleAsync(IdentityRole role)
        {
            try
            {
                await _identityService.CreateRoleAsync(role);

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("RoleExists")]
        public async Task<IActionResult> RoleExistsAsync(string roleName)
        {
            try
            {
                return Ok(await _identityService.RoleExistsAsync(roleName));
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddToRole")]
        public async Task<IActionResult> AddToRoleAsync(User user, string roleName)
        {
            try
            {
                await _identityService.AddToRoleAsync(user, roleName);

                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
