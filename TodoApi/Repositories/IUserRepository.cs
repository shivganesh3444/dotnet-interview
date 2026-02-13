using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        User CreateUser(User user);
    }
}