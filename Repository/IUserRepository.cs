using Models;
using Models.DTOs.Input;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<List<User>> GetFilteredUsers(UserFilterDTO filter);
        Task<int> Add(CreateUserDTO user);
        Task<User> Update(UpdateUserDTO user);
        Task<User> Delete(int id);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
