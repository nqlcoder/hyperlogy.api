using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Hyperlogy.Common.Entities;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class HyperAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public HyperAuthorizeAttribute()
    {

    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        //var user = context.HttpContext.Items["User"];
        var authorize = context.HttpContext.Request.Headers["Authorization"];
        var token = authorize.FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new JsonResult(new { messages = "Dữ liệu truyền thiếu Token.", code = 400, success = false }) { StatusCode = StatusCodes.Status400BadRequest };
            return;
        }
        var principal = AuthenticateJwtToken(token);
        if (principal.Result == null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Bạn không có quyền truy cập.", code = 401, success = false }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }
        context.HttpContext.Items["User"] = principal;
    }
    protected Task<IPrincipal> AuthenticateJwtToken(string token)
    {
        if (ValidateToken(token, out var publickey))
        {
            // based on username to get more information from database in order to build local identity
            var claims = new List<Claim>
                {
                    new Claim("PublicKey", publickey)
                    // Add more claims if needed: Roles, ...
                };

            var identity = new ClaimsIdentity(claims, "Jwt");
            IPrincipal user = new ClaimsPrincipal(identity);

            return Task.FromResult(user);
        }
        return Task.FromResult<IPrincipal>(null);
    }
    private bool ValidateToken(string token, out string publicKey)
    {
        publicKey = null;

        var simplePrinciple = GetPrincipal(token);
        var identity = simplePrinciple?.Identity as ClaimsIdentity;

        if (identity == null)
            return false;

        if (!identity.IsAuthenticated)
            return false;

        var publicKeyClaim = identity.FindFirst("publickey");
        var _pk = publicKeyClaim?.Value;
        publicKey = _pk;

        if (string.IsNullOrEmpty(publicKey))
            return false;

        // More validate to check whether username exists in system
        var user = TokenEntity.appUsers.FirstOrDefault(g => g.PublicKey == _pk);
        if (user == null)
        {
            return false;
        }
        return true;
    }
    public ClaimsPrincipal GetPrincipal(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;
            var key = WebJwtConstants.SECRET_KEY;
            var symmetricKey = Encoding.ASCII.GetBytes(key);

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return principal;
        }

        catch (Exception ex)
        {
            return null;
        }
    }
}