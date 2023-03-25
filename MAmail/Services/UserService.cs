using MAmail.Dtos;
using MAmail.Entities;
using MAmail.Mappings;
using MAmail.Repositories;

namespace MAmail.Services
{
    public class UserService
    {
        private UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public void CreateUser(UserCreateRequestDto userDto)
        {
            User user = UserMappingExtenstions.FromUserCreateRequestDto(userDto);

            userRepository.CreateUser(user);
        }
    }
}
