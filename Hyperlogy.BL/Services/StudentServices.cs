using Hyperlogy.BL.Interfaces;
using Hyperlogy.Common.Entities;
using Hyperlogy.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.BL.Services
{
    public class StudentServices : IStudentServices
    {
        protected readonly IStudentRepository _repository;

        public StudentServices(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Student>> GetAll()
        {
            List<Student> lstStudents = new List<Student>();

            try
            {
                lstStudents = await _repository.GetAll();
            }
            catch (Exception)
            {

            }
            return lstStudents;
        }

        public async Task<List<Student>> GetById(Guid studentId)
        {
            List<Student> lstStudents = new List<Student>();

            try
            {
                lstStudents = await _repository.GetById(studentId);
            }
            catch (Exception)
            {

            }
            return lstStudents;
        }

        public async Task<int> Insert(Student student)
        {
            var data = await _repository.Insert(student);
            return data;
        }

        public async Task<int> Update(Student student)
        {
            var data = await _repository.Update(student);
            return data;
        }
        public async Task<int> Delete(Guid studentId)
        {
            var data = await _repository.Delete(studentId);
            return data;
        }

        public async Task<object> Filter(string searchText, int pageSize, int pageNumber)
        {
            var data = await _repository.Filter(searchText, pageSize, pageNumber);
            return data;
        }
    }
}
