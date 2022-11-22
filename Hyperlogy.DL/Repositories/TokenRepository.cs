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
    public class TokenRepository : ITokenRepository, IDisposable
    {
        MySqlConnection _sqlConnection;
        public TokenRepository()
        {
            _sqlConnection = new MySqlConnection(Global.ConnectionString);
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
            _sqlConnection.Open();
        }

        public async Task<List<UserModel>> GetByUserName(string userName)
        {
            var sqlCommand = $"Select UserName from student UserName = '{userName}'";
            var data = await _sqlConnection.QueryAsync<UserModel>(sqlCommand);
            return data.ToList();
        }

        public async Task<List<UserModel>> GetByUserPassword(string password)
        {
            var sqlCommand = $"Select UserName from student UserName = '{password}'";
            var data = await _sqlConnection.QueryAsync<UserModel>(sqlCommand);
            return data.ToList();
        }
    }
}
