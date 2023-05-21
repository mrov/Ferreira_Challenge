using Models;
using Models.DTOs.Input;
using Utils;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<UserPagination> GetFilteredUsers(UserFilterDTO filter);
        Task<int> Add(CreateUserDTO user);
        Task<User> Update(UpdateUserDTO user);
        Task<User> Delete(int id);
        Task DeleteAllUsers();
        Task<User> GetUserByUsernameAsync(string username);
        Task<string> ResetPassword(User user);
        bool IsUserExists(string login);
    }
}
