using MAmail.Data;
using MAmail.Dtos;
using MAmail.Mappings;
using MAmail.Utils;
using Microsoft.EntityFrameworkCore;

namespace MAmail.Repositories
{
    public class AuthenticationRepository
    {
        private MAmailDBContext _db;
        private UserRepository _userRepository;
        private JWT _JWT;

        public AuthenticationRepository(MAmailDBContext db, UserRepository userRepository, JWT jwt)
        {
            _db = db;
            _userRepository = userRepository;
            _JWT = jwt;
        }

        public async Task<ActionInfo> Register(UserCreateRequestDto user)
        {
            var isEmailUsed = await _userRepository.GetUserByEmail(user.Email);

            if (isEmailUsed != null)
            {
                return new ActionInfo()
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

                return new ActionInfo()
                {
                    Success = true,
                    Message = "Success"
                };
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

                return new ActionInfo()
                {
                    Success = false,
                    Message = "Password can't be empty"
                };
            }
        }

        public async Task<ActionInfo> Login(UserLoginDto user)
        {
            var userByEmail = await _userRepository.GetUserByEmail(user.Email);

            if (userByEmail == null || !PasswordSecurity.VerifyHashedPassword(userByEmail.PasswordHash, user.Password))
            {
                return new ActionInfo()
                {
                    Success = false,
                    Message = "Invalid credentials",
                };
            }

            return new ActionInfo()
            {
                Success = true,
                Message = _JWT.GetToken(userByEmail)
            };
        }
    }
}
