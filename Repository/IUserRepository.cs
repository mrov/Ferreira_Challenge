using Models;
using Models.DTOs.Input;
using Utils;
using Utils.Enums;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<UserPagination> GetFilteredUsers(UserFilterDTO filter);
        Task<int> Add(CreateUserDTO user);
        Task<User> Update(UpdateUserDTO user);
        Task<Status> UpdateUserStatus(User user, Status newStatus);
        Task<User> Delete(int id);
        Task DeleteAllUsers();
        Task<User> GetUserByUsernameAsync(string username);
        Task<string> ResetPassword(User user);
        bool IsUserExists(string login);
        bool IsLoginExists(string newLogin);
    }
}
