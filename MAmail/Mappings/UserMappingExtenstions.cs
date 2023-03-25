using MAmail.Dtos;
using MAmail.Entities;

namespace MAmail.Mappings
{
    public static class UserMappingExtenstions
    {
        public static User FromUserCreateRequestDto(UserCreateRequestDto userDto)
        {
            User user = new User();
            user.Id = 0;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            // we have to hash it before saving
            user.PasswordHash = userDto.Password;
            user.CreatedDate = DateTime.Now;

            return user;
        }
    }
}
