using pfp.Application.Interfaces;

namespace pfp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; set; }

        public UnitOfWork(IUserRepository userRepository)
        {
            Users = userRepository;
        }
    }
}
