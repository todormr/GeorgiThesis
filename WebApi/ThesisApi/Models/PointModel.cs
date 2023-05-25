using System.ComponentModel.DataAnnotations;

namespace ThesisApi.Models
{
    public class PointModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }    
        public string Longitude { get; set; }
        public int UserId { get; set; }
        public UserModel UserName { get; set; } 

    }
}
