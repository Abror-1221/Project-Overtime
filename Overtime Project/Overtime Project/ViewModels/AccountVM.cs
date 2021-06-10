using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.ViewModels
{
    public class ChangePasswordVM
    {
        public string NIK { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }

    public class LogInVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AccountUserVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public int OvertimeHour { get; set; }
        public string Email { get; set; }
    }
}
