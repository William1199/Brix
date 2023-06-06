using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Users;

namespace Application.ClaimTokens
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }


        public Token(string accessToken, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        public Token()
        {

        }
    }

}
