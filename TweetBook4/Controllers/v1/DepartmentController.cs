using AutoMapper;
using EmployeeManagement.Contracts.v1.Query;
using EmployeeManagement.Contracts.v1.Requests;
using EmployeeManagement.Contracts.v1.Requests.Queries;
using EmployeeManagement.Contracts.v1.Responses;
using EmployeeManagement.Data;
using EmployeeManagement.Domain;
using EmployeeManagement.Helpers;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TweetBook4.Contracts.vi;

namespace EmployeeManagement.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _deptRepository;
        private readonly IMapper mapper;
        private readonly IUriService _uriService;
        public DepartmentController(IDepartmentRepository departmentRepository,IMapper mapper,IUriService uriService)
        {
            this._deptRepository = departmentRepository;
            this.mapper = mapper;
            this._uriService = uriService;
        }
        [HttpGet(ApiRoutes.Dept.Search)]
        public async Task<IActionResult> Search(string name)
        {
            try
            {
                var result = await _deptRepository.Search(name);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("No data found!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving department data from database!");
            }
        }

        [HttpGet(ApiRoutes.Dept.getAll)]
        public async Task<IActionResult> GetDepartments([FromQuery] DeptQuery DeptQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var paginationFilter = mapper.Map<PaginationFilter>(paginationQuery);
                var result = await _deptRepository.GetDepartments(DeptQuery, paginationFilter);
                if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageNumber < 1)
                {
                    return Ok(new PagedResponse<Department>(result));
                }
                
                var paginationResponse = PaginationHelpers.CreatePaginationResponse(_uriService, paginationFilter, result);
                return Ok(paginationResponse);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving department data from database!");
            }
        }

        [HttpGet(ApiRoutes.Dept.Get)]
        public async Task<IActionResult> GetDepartment([FromRoute] int id)
        {
            try
            {
                var result = await _deptRepository.GetDepartment(id);
                if (result == null)
                {
                    return NotFound($"Department with ID : {id} not found!");
                }
                return Ok(new Response<Department>(result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retriving Department with ID ={id} !");
            }
        }

        [HttpPost(ApiRoutes.Dept.Create)]
        public async Task<IActionResult> CreateDepartment([FromBody] DeptRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("unable to create department with null!");
                }
                var dept = mapper.Map<Department>(request);
                var createdDept = await _deptRepository.AddDepartment(dept);
                // to change uri 
                return CreatedAtAction(nameof(GetDepartment), new { id = createdDept.DeptId }, new Response<Department>(createdDept));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on creating new department record!");
            }
        }

        [HttpPut(ApiRoutes.Dept.Update)]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, DeptRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("unable to update department with null!");
                }
                var dept = await _deptRepository.GetDepartment(id);
                if (dept == null)
                {
                    return NotFound($"department with ID = {id} not found ");
                }
                var deptToUpdate = mapper.Map<Department>(request);
                deptToUpdate.DeptId = dept.DeptId;
                var updatedDept = await _deptRepository.UpdateDepartment(deptToUpdate);
                return Ok(new Response<Department>(updatedDept));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error on updating department with ID = {id} ");
            }
        }

        [HttpDelete(ApiRoutes.Dept.Delete)]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            try
            {
                var deptToUpdate = await _deptRepository.GetDepartment(id);
                if (deptToUpdate == null)
                {
                    return NotFound($"Department with ID = {id} not found ");
                }

                await _deptRepository.DeleteDepartment(id);
                return Ok($"Department with ID = {id} deleted successfully ");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error on deleting department with ID = {id} ");
            }
        }
    }
}
