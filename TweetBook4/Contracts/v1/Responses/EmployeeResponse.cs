using EmployeeManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Contracts.v1.Responses
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public string Image { get; set; }
        public Department Department { get; set; }
    }
}
