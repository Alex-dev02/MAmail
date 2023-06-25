using MAmail.Dtos;
using MAmail.Repositories;

namespace MAmail.Services
{
    public class AuthenticationService
    {
        private AuthenticationRepository _authenticationRepository;

        public AuthenticationService(AuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public async Task<ActionInfo> Register(UserCreateRequestDto user)
        {
            return await _authenticationRepository.Register(user);
        }

        public async Task<ActionInfo> Login(UserLoginDto user)
        {
            return await _authenticationRepository.Login(user);
        }
    }
}
