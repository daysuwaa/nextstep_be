namespace nextstep.application.DTOs.Responses
{
    public class LoginResponseModel
        {
            public  string Token { get; set; }
            public DateTime Expiration { get; set; }
            public Guid UserId { get; set; }
    }

    
}

