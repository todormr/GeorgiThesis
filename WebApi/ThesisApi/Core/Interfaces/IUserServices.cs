using ThesisApi.Models;

namespace ThesisApi.Core.Interfaces
{
    public interface IUserServices
    {
        IEnumerable<UserModel> GetUsers();  
        Task AddUser(UserModel user);   
        Task RemoveUser(int userId);
        UserModel AuthenticateUser(string userName, string password);
        string GenerateJSONWebToken(UserModel userInfo);
    }
}
