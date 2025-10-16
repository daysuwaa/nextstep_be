using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using nextstep.application.Abstractions.Core;
using nextstep.application.Abstractions.Service;
using nextstep.application.DTOs.Responses;
using nextstep.application.Interfaces;
using nextstep.domain.Entities;

namespace nextstep.application.Handlers
{
    // userhandler implements the interface
    public class UserHandler : IUserHandler
    {
        // injected database context (IAppDbContext) used to interact with your database.
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public UserHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // for register

        public async Task<User> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken = default)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.email == email, cancellationToken);

            if (existingUser != null)
                throw new InvalidOperationException("Email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                name = username,
                email = email,
                password = hashedPassword,
                createdAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);

            return newUser;
        }


        // for login

        public async Task<TokenResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == email, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.password))
                throw new UnauthorizedAccessException("Invalid credentials");

            // Generate a real JWT using TokenService
            var token = _tokenService.GenerateToken(user);

            // Return token + expiry + userId
            return new TokenResponse
            {
                Token = token.token,
                TokenExpiry = token.tokenExpiry,
                UserId = user.id
            };
        }

        public static int GetUserIdFromContext(HttpContext context)
        {
            var userIdClaim = context.User.FindFirst("id")?.Value
                            ?? context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                            ?? context.User.FindFirst("sub")?.Value;



            if (userIdClaim == null)
                throw new Exception("User ID not found in token bro.");

            return int.Parse(userIdClaim);
        }

    }
}