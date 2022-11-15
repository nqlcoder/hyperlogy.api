using Hyperlogy.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.BL.Interfaces
{
    public interface IStudentServices
    {
        Task<List<Student>> GetAll();
        Task<List<Student>> GetById(Guid studentId);
        Task<int> Insert(Student student);
        Task<int> Update(Student student);
        Task<int> Delete(Guid studentId);
        Task<object> Filter(string? searchText, int pageSize, int pageNumber);
    }
}
