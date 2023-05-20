using Models;
using Models.DTOs.Input;
using Models.DTOs.Output;

namespace Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<List<UserDTO>> GetFilteredUsers(UserFilterDTO filter);
        Task<int> CreateUser(CreateUserDTO user);
        Task<User> UpdateUser(UpdateUserDTO user);
        Task<User> DeleteUser(int id);
        Task DeleteAllUsers();
    }
}