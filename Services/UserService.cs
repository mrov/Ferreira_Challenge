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

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
            _userRepository.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
            _userRepository.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
            _userRepository.SaveChanges();
        }
    }
}
