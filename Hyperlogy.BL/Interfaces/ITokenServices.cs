using Hyperlogy.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.BL.Interfaces
{
    public interface ITokenServices
    {
        Task<List<UserModel>> GetByUserName(string userName);
        Task<List<UserModel>> GetByUserPassword(string password);
    }
}
