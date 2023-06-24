using MAmail.Dtos;
using MAmail.Entities;
using MAmail.Mappings;
using MAmail.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MAmail.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateUser(UserCreateRequestDto userDto)
        {
            User user = UserMappingExtenstions.FromUserCreateRequestDto(userDto);

            _userRepository.CreateUser(user);
        }

        public async Task<UserGetByIdResponseDto?> GetUserById(int userId)
        {
            User? user = await _userRepository.GetUserById(userId);

            if (user == null)
                return null;

            return UserMappingExtenstions.ToUserGetByIdResponseDto(user);
        }

        public async Task<List<User>?> GetUsersByEmail(List<string> emails)
        {
            return await _userRepository.GetUsersByEmail(emails);
        }

        public async Task<bool> UpdateUser(UserUpdateDto updatedUser)
        {
            var user = await _userRepository.GetUserById(updatedUser.Id);

            if (user == null)
                return false;

            _userRepository.UpdateUser(updatedUser, user);

            return true;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var userToDelete = await _userRepository.GetUserById(userId);

            if (userToDelete == null)
                return false;

            _userRepository.DeleteUser(userToDelete);

            return true;
        }
    }
}
