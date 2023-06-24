using MAmail.Data;
using MAmail.Dtos;
using MAmail.Entities;
using MAmail.Mappings;
using MAmail.Utils;
using Microsoft.EntityFrameworkCore;

namespace MAmail.Repositories
{
    public class AuthorizationRepository
    {
        private MAmailDBContext _db;

        public AuthorizationRepository(MAmailDBContext db)
        {
            _db = db;
        }

        public async Task<RegisterResponse> Register(UserCreateRequestDto user)
        {
            var isEmailUsed = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (isEmailUsed != null)
            {
                return new RegisterResponse()
                {
                    Success = false,
                    Message = "Email already used!"
                };
            }

            try
            {
                string hashedPassword = PasswordSecurity.HashPassword(user.Password);
                user.Password = hashedPassword;

                await _db.Users.AddAsync(UserMappingExtenstions.FromUserCreateRequestDto(user));
                _db.SaveChanges();

                return new RegisterResponse()
                {
                    Success = true,
                    Message = "Success"
                };
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

                return new RegisterResponse()
                {
                    Success = false,
                    Message = "Password can't be empty"
                };
            }
        }
    }
}
