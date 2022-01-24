using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using pfp.Core.Entities;
using pfp.Application.Interfaces;

namespace pfp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(User entity)
        {
            var sql = "INSERT INTO tbl_user (first_name, last_name, email, password) VALUES (@FirstName, @LastName, @Email, @Password);";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM tbl_user WHERE id = @id;";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { id = id });
                return affectedRows;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM tbl_user WHERE id = @id;";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { id = id });
                return result;
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM tbl_user WHERE email = @Email;";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM tbl_user WHERE deleted = 0;";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql);
                return result.ToList();
            }
        }

        public async Task<int> UpdateAsync(User entity)
        {
            var sql = "UPDATE tbl_user SET first_name = @FirstName, last_name = @LastName WHERE id = @id;";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
