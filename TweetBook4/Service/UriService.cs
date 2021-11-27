using EmployeeManagement.Contracts.v1.Requests.Queries;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook4.Contracts.vi;

namespace EmployeeManagement.Service
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAllDeptUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri+ ApiRoutes.Dept.getAll);
            if (pagination == null)
            {
                return uri;
            }
            var modifiedUri = QueryHelpers.AddQueryString(_baseUri + ApiRoutes.Dept.getAll, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetAllEmployeeUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri+ApiRoutes.Employee.getAll);
            if (pagination == null)
            {
                return uri;
            }
            var modifiedUri = QueryHelpers.AddQueryString(_baseUri + ApiRoutes.Employee.getAll, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetDeptUri(string postId)
        {
            return new Uri(_baseUri + ApiRoutes.Dept.Get.Replace("id", postId));
        }

        public Uri GetEmployeeUri(string postId)
        {
            return new Uri(_baseUri + ApiRoutes.Employee.Get.Replace("id", postId));
        }

    }
}
