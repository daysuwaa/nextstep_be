using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace nextstep.API.Helpers
{
    public static class UserHelper
    {
        public static int GetUserIdFromContext(HttpContext context)
        {
            var userIdClaim = context.User.FindFirst("id")?.Value
                            ?? context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                            ?? context.User.FindFirst("sub")?.Value;

            if (userIdClaim == null)
                throw new Exception("User ID not found in token bruv.");

            return int.Parse(userIdClaim); 
        }
    }
}