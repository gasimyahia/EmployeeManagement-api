using EmployeeManagement.Contracts.v1.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Service
{
    public interface IUriService
    {
        Uri GetEmployeeUri(string postId);
        Uri GetAllEmployeeUri(PaginationQuery pagination = null);
        Uri GetDeptUri(string postId);
        Uri GetAllDeptUri(PaginationQuery pagination = null);
    }
}
