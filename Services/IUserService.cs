using Models;

namespace Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> CreateUser();
        Task<IEnumerable<User>> EditUser();
        Task<IEnumerable<User>> DeleteUser();

    }
}