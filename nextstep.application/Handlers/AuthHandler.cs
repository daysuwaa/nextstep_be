using nextstep.application.Abstractions.Core;
using nextstep.application.Abstractions.Service;
using nextstep.application.InterFaces;
using nextstep.domain.Entities; 

namespace nextstep.application.Handlers
{
    // interface class
    public class AuthHandler : IAuthHandler
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthHandler(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        //public is the access modifier;  token is the variable, login is the method name, email and password are the paramenters
        public Token Login(string email, string password)
        {
            var user = _userRepository.GetSingleUser(email, password);

            if(user == null)
            {
                throw new UnauthorizedAccessException("Invalid Email or passsword");
            }

            // Generate token if user exists
            var generatedToken = _tokenService.GenerateToken(user);

            return generatedToken;
        }

    }

}

