using Models;
using Models.DTOs.Input;

namespace Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<int> CreateUser(CreateUserDTO user);
        Task<User> UpdateUser(UpdateUserDTO user);
        Task<User> DeleteUser(int id);
    }
}