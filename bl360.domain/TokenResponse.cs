using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.domain
{

    public class TokenRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserImageURL { get; set; }
        public string Messages { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
    }

    public class CompletedUserAuth
    {
        public User AuthenticatedUser { get; set; }
        public Company AuthenticatedCompany { get; set; }

        public CompletedUserAuth()
        {
            this.AuthenticatedCompany = new Company();
            this.AuthenticatedCompany.CompanyKey = 1;
            this.AuthenticatedUser = new User();
            this.AuthenticatedUser.UserKey = 1;
        }
    }


    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
