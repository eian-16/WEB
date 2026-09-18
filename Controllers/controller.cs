using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using studenrt.profileeian.Model;
using studenrt.profileeian.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace students_profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }



        [HttpPost("ognolok")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.UserName) ||
                string.IsNullOrEmpty(request.PassWord))
            {
                return BadRequest(new
                {
                    message = "error Username and password are required"
                });
            }

            var user = await _context.Logins
                .FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user == null || user.PassWord != request.PassWord)
            {
                await LogActivity(
                    "LOGIN_FAILED",
                    "Failed login attempt",
                    request.UserName
                );

                return BadRequest(new { message = "error" });
            }

            await LogActivity(
                "LOGIN",
                "User logged in successfully",
                user.UserName
            );

            return Ok(new { message = "success" });
        }



        [HttpPost("studentInfoRequest")]
        public async Task<IActionResult> Create(StudentInfoRequest request)
        {
            var student = new StudentInfo
            {
                First_Name = request.First_Name,
                Last_Name = request.Last_Name,
                Birth_Day = request.Birth_Day,
                Civil_Status = request.Civil_Status,
                Age = request.Age,
                Gender = request.Gender
            };

            _context.Student_info.Add(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "success!!" });
        }




        [HttpPost("registerFunction")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.PassWord))
            {
                return BadRequest(new { message = "error username and password are required" });
            }

            var existingUser = await _context.Logins.AnyAsync(u => u.UserName == request.UserName);
            if (existingUser)
            {
                return BadRequest(new { message = "error user already exists" });
            }

            var newUser = new Login
            {
                UserName = request.UserName,
                PassWord = request.PassWord,
            };

            _context.Logins.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "success!!" });
        }



        [HttpPut("studentInfoRequest/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentInfoRequest request)
        {
            var student = await _context.Student_info.FindAsync(id);

            if (student == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            student.First_Name = request.First_Name;
            student.Last_Name = request.Last_Name;
            student.Birth_Day = request.Birth_Day;
            student.Civil_Status = request.Civil_Status;
            student.Age = request.Age;
            student.Gender = request.Gender;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Student updated successfully!!" });
        }




        [HttpDelete("studentInfoDelete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Student_info.FindAsync(id);

            if (student == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            _context.Student_info.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student deleted successfully!!" });
        }

        private async Task LogActivity(
            string action,
            string description,
            string userName)
        {
            var activityLog = new ActivityLog
            {
                Action = action,
                Description = description,
                UserName = userName,
                CreatedAt = DateTime.Now
            };

            _context.ActivityLogs.Add(activityLog);
            await _context.SaveChangesAsync();
        }
    }


}

    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
    }
    public class StudentInfoRequest
    {
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public DateTime Birth_Day { get; set; }
        public string Civil_Status { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }



