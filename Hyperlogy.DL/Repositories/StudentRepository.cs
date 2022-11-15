using Dapper;
using Hyperlogy.Common.Entities;
using Hyperlogy.Common.Global;
using Hyperlogy.DL.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.DL.Repositories
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        protected MySqlConnection _sqlConnection;

        public StudentRepository()
        {
            _sqlConnection = new MySqlConnection(Global.ConnectionString);
            //kiem tra trang thai ket noi
            if(_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

        }
        
        public void Dispose()
        {
            _sqlConnection.Close();
            _sqlConnection.Dispose();
        }

        public async Task<List<Student>> GetAll()
        {
            //khai bao sqlCommand
            var sqlCommand = $"Select * FROM student";

            //thuc hien lay du lieu
            var data = await _sqlConnection.QueryAsync<Student>(sqlCommand);
            Dispose();
            return data.ToList();
        }

        public async Task<List<Student>> GetById(Guid studentId)
        {
            //khai bao sqlCommand
            var sqlCommand = $"Select * FROM student where StudentId = '{studentId}'";

            //thuc hien lay du lieu
            var data = await _sqlConnection.QueryAsync<Student>(sqlCommand);
            Dispose();
            return data.ToList();
        }

        public async Task<int> Insert(Student student)
        {
            //Ten thu tuc
            var sqlCommnad = $"Proc_Insert";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@StudentCode", student.StudentCode, DbType.String);
            parameters.Add("@StudentName", student.StudentName, DbType.String);
            parameters.Add("@Address", student.Address, DbType.String);
            parameters.Add("@Gender", student.Gender, DbType.Int32);
            parameters.Add("@DateOfBirth", student.DateOfBirth, DbType.String);
            parameters.Add("@ClassId", student.ClassId, DbType.String);

            var data = await _sqlConnection.ExecuteAsync(sqlCommnad, param: parameters, commandType: CommandType.StoredProcedure);
            Dispose();
            return data;
        }

        public async Task<int> Update(Student student)
        {
            //Ten thu tuc
            var sqlCommnad = $"Proc_Update";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@StudentId", student.StudentId);
            parameters.Add("@StudentCode", student.StudentCode);
            parameters.Add("@StudentName", student.StudentName);
            parameters.Add("@Address", student.Address);
            parameters.Add("@Gender", student.Gender);
            parameters.Add("@DateOfBirth", student.DateOfBirth);
            parameters.Add("@ClassId", student.ClassId);

            var data = await _sqlConnection.ExecuteAsync(sqlCommnad, param: parameters, commandType: CommandType.StoredProcedure);
            Dispose();
            return data;
        }
        public async Task<int> Delete(Guid studentId)
        {
            var sqlCommand = $"Delete from student where StudentId = '{studentId}'";
            var data = await _sqlConnection.ExecuteAsync(sqlCommand);
            Dispose();
            return data;
        }

        public async Task<object> Filter(string searchText, int pageSize, int pageNumber)
        {
            var storeName = $"Proc_FilterStudent";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@searchText", searchText);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);

            parameters.Add("@TotalRecord", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@RecordStart", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@RecordEnd", DbType.Int32, direction: ParameterDirection.Output);

            var data = await _sqlConnection.QueryAsync<Student>(storeName, param: parameters, commandType: CommandType.StoredProcedure);

            int totalRecord = parameters.Get<int>("@TotalRecord");
            int totalPage = parameters.Get<int>("@TotalPage");
            int recordStart = parameters.Get<int>("@RecordStart");
            int recordEnd = parameters.Get<int>("@RecordEnd");

            Dispose();
            return new
            {
                TotalPage = totalPage,
                TotalRecord = totalRecord,
                RecordStart = recordStart,
                RecordEnd = recordEnd,
                Data = data
            };
        }
    }
}
