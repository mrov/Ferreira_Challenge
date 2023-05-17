using Models;
using Models.DTOs.Input;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetAll();
        Task<int> Add(CreateUserDTO user);
        Task<User> Update(User user);
        Task<User> Delete(int id);
        void SaveChanges();
    }
}
