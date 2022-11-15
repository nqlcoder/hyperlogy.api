using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hyperlogy.Common.JWT;
using Hyperlogy.Common.Entities;

namespace Hyperlogy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        [HttpGet]
        [Route("GetToken")]
        public string GetToken ()
        {
            string token = string.Empty;
            // get user by username
            //var getUser = userService.getUserByUsername(model.UserName);
            //// neu user ton tai
            //if (getUser != null)
            //{ 
            //    // kiem tra password truyen vao co khop khong
            //    // param1: password ng dung nhap
            //    // param2: password da dc luu trong DB
            //    bool verified = BCrypt.Net.BCrypt.Verify(model.Password, getUser.Password);
            //    // neu password trung khop thi tao token
            //    if (verified) { 
            //        // create token
            //        token = TokenMananger.GenerateToken(model.UserName, "affafaf13");
            //    } 
            //}
            token = TokenMananger.GenerateToken("aff", "affafaf13");
            // tra ve token
            return token;
        }
    }
}
