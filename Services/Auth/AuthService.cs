using Models;
using Repository;
using Services.Auth;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResult> LoginAsync(string username, string password)
        {
            try
            {

                var user = await _userRepository.GetUserByUsernameAsync(username);

                if (user == null || !VerifyPassword(password, user.Password))
                {
                    return AuthResult.Failure("Invalid username or password");
                }

                var token = _jwtService.GenerateJwtToken(user);

                return AuthResult.Success(token);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCryptNet.Verify(password, passwordHash);
        }
}
}
