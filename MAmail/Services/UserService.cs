using MAmail.Dtos;
using MAmail.Entities;
using MAmail.Mappings;
using MAmail.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public UserGetByIdResponseDto? GetUserById(int userId)
        {
            User? user = userRepository.GetUserById(userId);

            if (user == null)
                return null;

            return UserMappingExtenstions.ToUserGetByIdResponseDto(user);
        }
        public bool UpdateUser(UserUpdateDto updatedUser)
        {
            var user = userRepository.GetUserById(updatedUser.Id);

            if (user == null)
                return false;

            userRepository.UpdateUser(updatedUser, user);

            return true;
        }
        public bool DeleteUser(int userId)
        {
            var userToDelete = userRepository.GetUserById(userId);

            if (userToDelete == null)
                return false;

            userRepository.DeleteUser(userToDelete);

            return true;
        }
    }
}
