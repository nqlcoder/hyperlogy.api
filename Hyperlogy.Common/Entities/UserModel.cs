using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.Common.Entities
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class TokenEntity
    {
        public class UserToken
        {
            public string UserName { get; set; }
            public string PublicKey { get; set; }
        }

        public static List<UserToken> appUsers = new List<UserToken>()
        {
            new UserToken { UserName = "hyper", PublicKey = "Demo12345" },  // chi dung cho Demo
            new UserToken { UserName = "hyperdev", PublicKey = "Hyper1234" },  // dung de phat trien
        };
    }

}
