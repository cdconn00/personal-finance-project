using pfp.Core.Entities;

namespace pfp.Application.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetByEmailAsync(string email);
    }
}
