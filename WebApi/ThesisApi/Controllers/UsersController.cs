using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisApi.Core.Interfaces;
using ThesisApi.Models;

namespace ThesisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserServices _userServices;
        public UsersController(IConfiguration config, IUserServices userServices)
        {
            _userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser([FromForm] string userName, [FromForm] string password, [FromForm] string email)
        {
            _userServices.AddUser(new UserModel()
            {
                EmailAddress = email,
                Username = userName,
                Password = password
            });

            return Ok();
        }
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<UserModel> Listusers()
        {
            return _userServices.GetUsers();

        }
    }
}
