using MAmail.Dtos;
using MAmail.Repositories;
using MAmail.Utils;

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
    }
}
