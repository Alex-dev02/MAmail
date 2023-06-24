using MAmail.Dtos;
using MAmail.Repositories;

namespace MAmail.Services
{
    public class AuthorizationService
    {
        private AuthorizationRepository _authorizationRepository;

        public AuthorizationService(AuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<RegisterResponse> Register(UserCreateRequestDto user)
        {
            return await _authorizationRepository.Register(user);
        }

        public async Task<LoginResponse> Login(UserLoginDto user)
        {
            return await _authorizationRepository.Login(user);
        }
    }
}
