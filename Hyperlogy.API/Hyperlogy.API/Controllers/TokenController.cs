using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hyperlogy.Common.JWT;
using Hyperlogy.Common.Entities;
using Hyperlogy.BL.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Hyperlogy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        //protected readonly ITokenServices _services;

        public TokenController()
        {
            //_services = services;
        }

        

        [HttpGet]
        [Route("GetToken")]
        public string GetToken (string userName)
        {
            string token = string.Empty;
            //get user by username
            //var getUser = _services.GetByUserName(userModel.UserName).Result;
            //var getPassword = _services.GetByUserPassword(userModel.Password);
            //// neu user ton tai
            //if (getUser != null)
            //{
            //    // kiem tra password truyen vao co khop khong
            //    // param1: password ng dung nhap
            //    // param2: password da dc luu trong DB
            //    bool verified = BCrypt.Net.BCrypt.Verify(userModel.Password, "admin");
            //    // neu password trung khop thi tao token
            //    if (verified)
            //    {
            //        // create token
            //        token = TokenMananger.GenerateToken(getUser.FirstOrDefault(), "affafaf13");
            //    }
            //}
            var userExist = TokenEntity.appUsers.Where(g => g.UserName == userName).FirstOrDefault();
            token = TokenMananger.GenerateToken(userExist.UserName, userExist.PublicKey);
            // tra ve token
            return token;
        }

        [HttpGet]
        [ServiceFilter(typeof(HyperAuthorizeAttribute))]
        [Route("GetValue")]
        public string GetValue()
        {
            var val = "abc";
            return val;
        }
    }
}
