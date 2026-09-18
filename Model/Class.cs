

using studenrt.profileeian.Model;
using System.ComponentModel.DataAnnotations;

namespace studenrt.profileeian.Model
{
    public class Login

    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
    }

    public record Profile

    {
        [Key]
      
        public string Laast_Name { get; set; } = string.Empty;
     
        public string First_Name { get; set; } = string.Empty;
        public string Profile_pic { get; set; } = string.Empty;
    }

    public class StudentInfo

    {
        [Key]
        public int Info_ID { get; set; }

     
        public string First_Name { get; set; } = string.Empty;
      
        public string Last_Name { get; set; } = string.Empty;
        public DateTime Birth_Day { get; set; }

        public string Civil_Status { get; set; } = string.Empty;

        public string Age { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

    }

    public class ActivityLog
    {
        public int Id { get; set; }

        public string Action { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
