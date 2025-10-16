using System;
using nextstep.application.InterFaces;
using nextstep.domain.Entities;
using nextstep.application.DTOs.Responses;
using Microsoft.EntityFrameworkCore;


namespace nextstep.infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all users
        public List<User> GetAllUsers()
        {
            var appUsers = _context.Users.ToList();

            var domainUsers = appUsers.Select(u => new User
            {
                id = u.id,
                name = u.name,
                email = u.email,
                password = u.password,
                createdAt = u.createdAt,
                updatedAt = u.updatedAt
            }).ToList();

            return domainUsers;
        }

        // Get a single user by email and password
        public User? GetSingleUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.email == email);
            if (user == null)
                return null;

            // Compare hashed passwords
            var passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.password);
            return passwordMatch ? user : null;
        }
    }
}

