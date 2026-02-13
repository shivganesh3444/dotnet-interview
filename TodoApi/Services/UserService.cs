using System.Security.Cryptography;
using System.Text;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Register(string username, string password, string role);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        public User Register(string username, string password, string role)
        {
            var passwordHash = HashPassword(password);
            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                Role = role
            };
            return _userRepository.CreateUser(user);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}