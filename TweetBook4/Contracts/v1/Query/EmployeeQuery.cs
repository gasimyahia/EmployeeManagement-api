using EmployeeManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Contracts.v1.Query
{
    public class EmployeeQuery
    {
        public int? DeptId { get; set; }
        public Gender? Gender { get; set; }
    }
}
