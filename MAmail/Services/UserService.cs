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
        public async Task<UserGetByIdResponseDto?> GetUserById(int userId)
        {
            User? user = await userRepository.GetUserById(userId);

            if (user == null)
                return null;

            return UserMappingExtenstions.ToUserGetByIdResponseDto(user);
        }
        public async Task<bool> UpdateUser(UserUpdateDto updatedUser)
        {
            var user = await userRepository.GetUserById(updatedUser.Id);

            if (user == null)
                return false;

            userRepository.UpdateUser(updatedUser, user);

            return true;
        }
        public async Task<bool> DeleteUser(int userId)
        {
            var userToDelete = await userRepository.GetUserById(userId);

            if (userToDelete == null)
                return false;

            userRepository.DeleteUser(userToDelete);

            return true;
        }
    }
}
