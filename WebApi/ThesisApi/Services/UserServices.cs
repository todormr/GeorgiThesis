using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThesisApi.Core.Context;
using ThesisApi.Core.Interfaces;
using ThesisApi.Models;

namespace ThesisApi.Services
{
    public class UserServices : IUserServices
    {
        private EvServiceContext _evServiceContext;
        private IConfiguration _config;
        public UserServices(EvServiceContext db, IConfiguration config)
        {
            _config= config;
            _evServiceContext = db;
        }
        public async Task AddUser(UserModel user)
        {
            await _evServiceContext.AddAsync(user);
            await _evServiceContext.SaveChangesAsync();

        }

        public IEnumerable<UserModel> GetUsers()
        {

            return _evServiceContext.Users.AsEnumerable();
        }

        public Task RemoveUser(int userId)
        {
//            var findUser = _evServiceContext.Users.Where(pr=>pr.id)
            return Task.CompletedTask;
        }
        public UserModel AuthenticateUser(string userName, string password)
        {
            var getCurrentUser =_evServiceContext.Users.Where(pr=>pr.Username == userName && pr.Password == password).FirstOrDefault();
            if (getCurrentUser != null)
            {
                return getCurrentUser; // new UserModel { Username = getCurrentUser.Username,  EmailAddress = getCurrentUser.EmailAddress};
            }
            return null;
        }
        public string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("userId", userInfo.Id.ToString()),
                new Claim("name", userInfo.Username.ToString()),
         
         
               //More custom claims
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],claims:claims,null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
