using Entities;

namespace Service.user
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUsreById(int id);
        Task<User> Login(string userName, string password);
        Task<User> Register(User user);
        Task<User> UpdateUser(int id, User user);
         Task<User> getUser(string email, string password);

    }
}