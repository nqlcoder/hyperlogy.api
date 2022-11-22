using Hyperlogy.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.DL.Interfaces
{
    public interface ITokenRepository
    {
        Task<List<UserModel>> GetByUserName(string userName);
        Task<List<UserModel>> GetByUserPassword(string password);
    }
}
