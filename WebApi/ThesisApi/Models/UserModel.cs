using System.ComponentModel.DataAnnotations;

namespace ThesisApi.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; } 
      
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsEnable { get; set; }
        public string Username { get; set; }
    }
}
