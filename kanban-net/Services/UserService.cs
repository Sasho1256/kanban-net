using System.Data;
using kanban_net.Data;
using kanban_net.DTOs;
using kanban_net.Models;
using static kanban_net.Models.User;
using Microsoft.AspNetCore.Identity;

namespace kanban_net.Services
{
    public class UserService(KanbanContext db)
    {
        private readonly KanbanContext _db = db;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

        public UserDTO Create(UserCreateDTO data)
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
                Role = Enum.TryParse<UserRole>(data.Role.Trim(), true, out var r) ? r : UserRole.USER,
                Status = Enum.TryParse<UserStatus>(data.Status.Trim(), true, out var s) ? s : UserStatus.ACTIVE,
            };

            user.Password = _hasher.HashPassword(user, data.Password);

            _db.Users.Add(user);
            _db.SaveChanges();

            return new UserDTO(user);
        }

        public UserDTO Get(long id)
        {
            User? user = _db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new MissingMemberException("User not found.");
            }

            return new UserDTO(user);
        }

        public UserDTO[] GetAll()
        {
            List<User> users = _db.Users.ToList();

            if (users.Count == 0)
            {
                return [];
            }

            return users.Select(u => new UserDTO(u)).ToArray();
        }

        public UserDTO Update(long id, UserUpdateDTO data)
        {
            User? user = _db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new MissingMemberException("User not found.");
            }

            string? username = data.Username?.Trim();
            if (username != null)
            {
                if (_db.Users.Any(u => u.Username == username && u.Id != id))
                {
                    throw new DuplicateNameException("Username already taken.");
                }

                user.Username = username;
            }

            string? email = data.Email?.Trim();
            if (email != null)
            {
                if (_db.Users.Any(u => u.Email == email && u.Id != id))
                {
                    throw new DuplicateNameException("Email already registered.");
                }

                user.Email = email;
            }

            string? role = data.Role?.Trim();
            if (role != null && Enum.TryParse<UserRole>(role, true, out var r))
            {
                user.Role = r;
            }

            string? status = data.Status?.Trim();
            if (status != null && Enum.TryParse<UserStatus>(data.Status, true, out var s))
            {
                user.Status = s;
            }

            string? password = data.Password;
            if (password != null)
            {
                user.Password = _hasher.HashPassword(user, password);
            }

            _db.SaveChanges();

            return new UserDTO(user);
        }

        public void Delete(long id)
        {
            User? user = _db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new MissingMemberException("User not found.");
            }

            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}