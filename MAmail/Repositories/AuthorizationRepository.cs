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

            await _db.Users.AddAsync(UserMappingExtenstions.FromUserCreateRequestDto(user));

            return new RegisterResponse()
            {
                Success = true,
                Message = "Success"
            };
        }
    }
}
