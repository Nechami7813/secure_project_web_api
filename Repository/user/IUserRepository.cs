using Entities;

namespace Repository.user
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get();
        Task<User> GetById(int id);
        Task<User> Register(User user);

        Task<User> Login(string userName, string password);
        Task<User> Put(int id, User user);
        Task<User> getUser(string email, string password);
    }
}