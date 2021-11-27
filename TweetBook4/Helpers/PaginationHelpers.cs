using EmployeeManagement.Contracts.v1.Requests.Queries;
using EmployeeManagement.Contracts.v1.Responses;
using EmployeeManagement.Domain;
using EmployeeManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginationResponse<T>(IUriService _uriService, PaginationFilter paginationFilter, List<T> responses)
        {
            var nextPage = paginationFilter.PageNumber >= 1 ? _uriService.GetAllEmployeeUri(
                    new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString() : null;
            var PreviousPage = paginationFilter.PageNumber - 1 >= 1 ? _uriService.GetAllEmployeeUri(
                new PaginationQuery(paginationFilter.PageNumber - 1, paginationFilter.PageSize)).ToString() : null;

            return new PagedResponse<T>
            {
                Data = responses,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : (int?)null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : (int?)null,
                NextPage = responses.Any() ? nextPage : null,
                PreviousPage = PreviousPage
            };
        }
    }
}
