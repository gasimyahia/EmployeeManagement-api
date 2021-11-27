using AutoMapper;
using EmployeeManagement.Contracts.v1.Requests;
using EmployeeManagement.Contracts.v1.Requests.Queries;
using EmployeeManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.MappingProfile
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<EmployeeRequest, Employee>()
                .ForMember(d => d.PhotoPath,opt=> opt.MapFrom(s=> s.Image))
                .ForMember(d=> d.DateOfBrith,opt=>opt.MapFrom(s=> s.DateOfBrith));

            CreateMap<DeptRequest, Department>()
                .ForMember(d=>d.DeptName,opt=> opt.MapFrom(s=> s.name));

            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
