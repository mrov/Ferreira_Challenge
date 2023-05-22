namespace Models.DTOs.Output
{
    public class AuthResult
    {
        public string Token { get; }
        public bool Status { get; }
        public string ErrorMessage { get; }

        private AuthResult(bool success, string token, string errorMessage)
        {
            Status = success;
            Token = token;
            ErrorMessage = errorMessage;
        }

        public static AuthResult Success(string token)
        {
            return new AuthResult(true, token, null);
        }

        public static AuthResult Failure(string errorMessage)
        {
            return new AuthResult(false, null, errorMessage);
        }
    }
}