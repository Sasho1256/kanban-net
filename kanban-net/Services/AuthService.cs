using System.Data;
using System.Security.Authentication;
using kanban_net.Data;
using kanban_net.DTOs;
using kanban_net.Models;
using Microsoft.AspNetCore.Identity;

namespace kanban_net.Services
{
    public class AuthService(KanbanContext db, JwtService jwtService)
    {
        private readonly KanbanContext _db = db;
        private readonly JwtService _jwtService = jwtService;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

        public TokenDTO Register(RegisterDTO data)
        {
            string username = data.Username.Trim();
            string? email = data.Email?.Trim();

            if (_db.Users.Any(u => u.Username == username))
            {
                throw new DuplicateNameException("Username already taken.");
            }

            if (email != null && _db.Users.Any(u => u.Email == email))
            {
                throw new DuplicateNameException("Email already registered.");
            }

            User user = new User()
            {
                Username = username,
                Email = email,
            };

            user.Password = _hasher.HashPassword(user, data.Password);

            _db.Users.Add(user);
            _db.SaveChanges();

            return new TokenDTO(_jwtService.GenerateToken(user));
        }

        public TokenDTO Login(LoginDTO data)
        {
            string username = data.Username.Trim();

            User? user = _db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new MissingMemberException("Username not found.");
            }

            string hash = user.Password!;

            var result = _hasher.VerifyHashedPassword(user, hash, data.Password);

            if (result != PasswordVerificationResult.Success)
            {
                throw new InvalidCredentialException("Wrong password.");
            }

            return new TokenDTO(_jwtService.GenerateToken(user));
        }
    }
}