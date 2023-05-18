using Models;
using Models.DTOs.Input;
using Repository;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<List<User>> GetFilteredUsers(UserFilterDTO filter)
        {
            return await _userRepository.GetFilteredUsers(filter);
        }

        public async Task<int> CreateUser(CreateUserDTO user)
        {
            try
            {
                var userId = await _userRepository.Add(user);

                return userId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public async Task<User> UpdateUser(UpdateUserDTO user)
        {
            return await _userRepository.Update(user);
        }

        public async Task<User> DeleteUser(int id)
        {
            return await _userRepository.Delete(id);
        }

    }
}
