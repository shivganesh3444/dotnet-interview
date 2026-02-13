using Microsoft.Data.Sqlite;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Data Source=todos.db";
        }

        public User GetUserByUsername(string username)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE Username = @Username";
            command.Parameters.AddWithValue("@Username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    PasswordHash = reader.GetString(2),
                    Role = reader.GetString(3)
                };
            }

            return null;
        }

        public User CreateUser(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Users (Username, PasswordHash, Role)
                VALUES (@Username, @PasswordHash, @Role);
                SELECT last_insert_rowid();
            ";
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@Role", user.Role);

            var id = Convert.ToInt32(command.ExecuteScalar());
            user.Id = id;
            return user;
        }
    }
}