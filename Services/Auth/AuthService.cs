using Models;
using Models.DTOs.Output;
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

                if (user.Status == Utils.Enums.Status.Inactive || user.Status == Utils.Enums.Status.Blocked)
                {
                    return AuthResult.Failure("User not found");
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

        public async Task<string> RecoverPassword(string login, string email, DateTime dateOfBirth)
        {
            try
            {
                // Retrieve the user based on the provided login
                User user = await _userRepository.GetUserByUsernameAsync(login);

                // Check if the user exists and the provided email and date of birth match
                if (user == null || user.Email != email || user.DateOfBirth.Date != dateOfBirth.Date)
                {
                    // Throw an exception or return an appropriate error message
                    throw new Exception("Invalid credentials for password recovery");
                }

                string newPassword = await _userRepository.ResetPassword(user);

                // Return the new password
                    return newPassword;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw;
            }

        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCryptNet.Verify(password, passwordHash);
        }
}
}
