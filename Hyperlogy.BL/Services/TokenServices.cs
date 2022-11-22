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
    public class TokenServices : ITokenServices
    {
        protected readonly ITokenRepository _repository;

        public TokenServices(ITokenRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserModel>> GetByUserName(string userName)
        {
            List<UserModel> lstUserModel = new List<UserModel>();

            try
            {
                lstUserModel = await _repository.GetByUserName(userName);
            }
            catch (Exception)
            {

            }
            return lstUserModel;
        }

        public async Task<List<UserModel>> GetByUserPassword(string password)
        {
            List<UserModel> lstUserModel = new List<UserModel>();

            try
            {
                lstUserModel = await _repository.GetByUserPassword(password);
            }
            catch (Exception)
            {

            }
            return lstUserModel;
        }
    }
}
