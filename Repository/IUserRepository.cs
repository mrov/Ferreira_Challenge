using Models;
using Models.DTOs.Input;
using Models.DTOs.Output;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<List<UserDTO>> GetFilteredUsers(UserFilterDTO filter);
        Task<int> Add(CreateUserDTO user);
        Task<User> Update(UpdateUserDTO user);
        Task<User> Delete(int id);
        Task DeleteAllUsers();
        Task<User> GetUserByUsernameAsync(string username);
        Task<string> ResetPassword(User user);
    }
}
