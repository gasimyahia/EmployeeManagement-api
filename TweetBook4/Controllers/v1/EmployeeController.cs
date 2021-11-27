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
using System.Threading.Tasks;
using TweetBook4.Contracts.vi;

namespace EmployeeManagement.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _empRepository;
        private readonly IDepartmentRepository _deptRepository;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository ,IMapper mapper, IUriService uriService)
        {
            this._empRepository = employeeRepository;
            this._deptRepository = departmentRepository;
            this._mapper = mapper;
            this._uriService = uriService;
        }

        [HttpGet(ApiRoutes.Employee.Search)]
        public async Task<IActionResult> Search(string name,Gender? gender)
        {
            try
            {
                var result = await _empRepository.Search(name,gender);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("No data found!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving employees data from database!");
            }
        }

        [HttpGet(ApiRoutes.Employee.getAll)]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeQuery postQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
                var result = await _empRepository.GetEmployees(postQuery, paginationFilter);
                if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageNumber < 1)
                {
                    return Ok(new PagedResponse<Employee>(result));
                }
                var paginationResponse = PaginationHelpers.CreatePaginationResponse(_uriService, paginationFilter, result);
                return Ok(paginationResponse);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving employees data from database!");
            }
        }

        [HttpGet(ApiRoutes.Employee.Get)]
        public async Task<IActionResult> GetEmployee([FromRoute]int id)
        {
            try
            {
                var result= await _empRepository.GetEmployee(id);
                if(result == null)
                {
                    return NotFound($"Employee with ID : {id} not found!");
                }
                return Ok(new Response<Employee>(result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retriving employee with ID ={id} !");
            }
        }

        [HttpPost(ApiRoutes.Employee.Create)]
        public async Task<IActionResult> CreateEmployee(EmployeeRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("unable to create employee with null!");
                }
                var dept = await _deptRepository.GetDepartment(request.DeptId);
                if(dept== null)
                {
                    ModelState.AddModelError("Dept id", $"Department with ID ={request.DeptId} not exists");
                    return BadRequest(ModelState);
                }
                var empl = await _empRepository.GetEmployeeByEmail(request.Email);
                if(empl != null)
                {
                    ModelState.AddModelError("Email", "Employee Email is already is use");
                    return BadRequest(ModelState);
                }
                var emp = _mapper.Map<Employee>(request);
                var createdEmployee = await _empRepository.AddEmployee(emp);
                // to change uri 
                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, new Response<Employee>(createdEmployee));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on creating new employee record!");
            }
        }

        [HttpPut(ApiRoutes.Employee.Update)]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, EmployeeRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("unable to update employee with null!");
                }
                var dept = await _deptRepository.GetDepartment(request.DeptId);
                if (dept == null)
                {
                    ModelState.AddModelError("Dept id", $"Department with ID ={request.DeptId} not exists");
                    return BadRequest(ModelState);
                }
                var empl = await _empRepository.GetEmployee(id);
                if (empl == null)
                {
                    return NotFound($"Employee with ID = {id} not found ");
                }
                var emp = _mapper.Map<Employee>(request);
                emp.EmployeeId = empl.EmployeeId;
                var updatedEmployee = await _empRepository.UpdateEmployee(emp);
                return Ok(new Response<Employee>(updatedEmployee));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error on updating employee with ID = {id} ");
            }
        }

        [HttpDelete(ApiRoutes.Employee.Delete)]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            try
            {
                var empToDelete =await _empRepository.GetEmployee(id);
                if (empToDelete == null)
                {
                    return NotFound($"Employee with ID = {id} not found ");
                }
               
                 await _empRepository.DeleteEmployee(id);
                return Ok($"Employee with ID = {id} deleted successfully ");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error on deleting employee with ID = {id} ");
            }
        }
    }
}
