using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ThesisApi.Core.Interfaces;
using ThesisApi.DTO;
using ThesisApi.Models;

namespace ThesisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserServices _userServices;
        public LoginController(IConfiguration config,IUserServices userServices)
        {
            _userServices = userServices;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]UserLoginDto login)
        {
            IActionResult response = Unauthorized();
            var user = _userServices.AuthenticateUser(login.UserName, login.Password);

            if (user != null)
            {
                var tokenString = _userServices.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            } 

            return response;
        }




    }
}
