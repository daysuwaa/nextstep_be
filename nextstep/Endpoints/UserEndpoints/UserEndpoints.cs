
namespace nextstep.Endpoints.UserEndpoints
{
    public static class UserModule
    {
        public static void AddUserEndpoint(this IEndpointRouteBuilder app)
        {
          
            // to get user details
            app.MapGet("api/v1/me", (int userid)  =>
            {
                var user = new
                {
                    Id = userid,
                    Name = "adesuwa",
                    Email = "adesuwa@email.com"
                };
                return Results.Ok("user added Successful" + userid);
            });
        }
    }
}

