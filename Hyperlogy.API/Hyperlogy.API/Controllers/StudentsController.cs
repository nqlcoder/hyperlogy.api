using Hyperlogy.BL.Interfaces;
using Hyperlogy.Common.Entities;
using Hyperlogy.Common.ResponeData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperlogy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        protected readonly IStudentServices _services;

        public StudentsController(IStudentServices services)
        {
            _services = services;
        }

        [HttpGet("GetAll")]
        public async Task<ResponeData> GetAll()
        {
            var data = new ResponeData();
            try
            {
                
                var res = await _services.GetAll();
                data.Students = res;
            }
            catch (Exception ex)
            {
                data.Code = 500;
                data.messages = ex.Message;
            }
            return data;
        }

        [HttpGet("GetById")]
        public async Task<ResponeData> GetById(Guid studentId)
        {
            var data = new ResponeData();
            try
            {
                var res = await _services.GetById(studentId);
                data.Students = res;
            }
            catch (Exception ex)
            {
                data.Code = 500;
                data.messages = ex.Message;
            }
            return data;
        }

        [HttpPost]
        [Route("PostNewStudent")]
        public async Task<IActionResult> Insert(Student student)
        {
            try
            {
                if (string.IsNullOrEmpty(student.StudentCode))
                {
                    var error = new
                    {
                        userMsg = "Ma NV khong duoc phep de trong"
                    };
                    return StatusCode(500, error);
                }

                if (string.IsNullOrEmpty(student.StudentName))
                {
                    var error = new
                    {
                        userMsg = "Ten NV khong duoc phep de trong"
                    };
                    return StatusCode(500, error);
                }

                var data = await _services.Insert(student);
                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("PutNewStudent")]
        public async Task<IActionResult> Update(Student student)
        {
            try
            {
                if (string.IsNullOrEmpty(student.StudentCode))
                {
                    var error = new
                    {
                        userMsg = "Ma NV khong duoc phep de trong"
                    };
                    return StatusCode(500, error);
                }

                if (string.IsNullOrEmpty(student.StudentName))
                {
                    var error = new
                    {
                        userMsg = "Ten NV khong duoc phep de trong"
                    };
                    return StatusCode(500, error);
                }

                var data = await _services.Update(student);
                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteStudent")]
        public async Task<IActionResult> Delete(Guid studentId)
        {
            try
            {
                var res = await _services.Delete(studentId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("FilterStudent")]
        public async Task<IActionResult> Filter(string? searchText, int pageSize, int pageNumber)
        {
            try
            {
                var data = await _services.Filter(searchText, pageSize, pageNumber);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void abc ()
        {
            var a = 1;
        }
        private void abc2()
        {
            var a = 1;
        }
    }
}
