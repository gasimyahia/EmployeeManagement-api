using EmployeeManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Contracts.v1.Requests
{
    public class EmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public string Image { get; set; }
        public int DeptId { get; set; }
    }
}
