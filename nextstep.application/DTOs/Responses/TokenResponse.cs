using System;
namespace nextstep.application.DTOs.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
        public int UserId { get; set; } 
    }
}

