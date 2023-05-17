using Models;
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<int> CreateUser(User user)
        {
            try
            {
                var userId = await _userRepository.Add(user);
                _userRepository.SaveChanges();

                return userId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<User> UpdateUser(User user)
        //{
        //    _userRepository.Update(user);
        //    _userRepository.SaveChanges();
        //}

        //public async Task<User> DeleteUser(int id)
        //{
        //    _userRepository.Delete(id);
        //    _userRepository.SaveChanges();
        //}
    }
}
