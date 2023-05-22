using Models;
using Models.DTOs.Input;
using Repository;
using Utils;
using Utils.Enums;

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
            return await _userRepository.GetUserById(id);
        }

        public async Task<UserPagination> GetFilteredUsers(UserFilterDTO filter)
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

        public async Task<Status> UpdateUserStatus(int id, Status status)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                throw new InvalidOperationException($"User with ID {id} not found");

            return await _userRepository.UpdateUserStatus(user, status);
        }

        public async Task<User> DeleteUser(int id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task DeleteAllUsers()
        {
            await _userRepository.DeleteAllUsers();
        }


        public bool IsUserExists(string login)
        {
            return _userRepository.IsUserExists(login);
        }

        public async Task<bool> CanUpdate(int id, string newLogin)
        {
            var existingUser = await _userRepository.GetUserById(id);

            if (newLogin == existingUser.Login)
            {
                return true;
            }

            if (newLogin != existingUser.Login && !_userRepository.IsLoginExists(newLogin))
            {
                return true;
            }

            return false;
        }


    }
}
