using Models;
using Models.DTOs.Input;

namespace Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<List<User>> GetFilteredUsers(UserFilterDTO filter);
        Task<int> CreateUser(CreateUserDTO user);
        Task<User> UpdateUser(UpdateUserDTO user);
        Task<User> DeleteUser(int id);
    }
}