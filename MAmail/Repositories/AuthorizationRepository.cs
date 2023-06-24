using MAmail.Data;
using MAmail.Dtos;
using MAmail.Mappings;
using MAmail.Utils;
using Microsoft.EntityFrameworkCore;

namespace MAmail.Repositories
{
    public class AuthorizationRepository
    {
        private MAmailDBContext _db;
        private UserRepository _userRepository;
        private JWT _JWT;

        public AuthorizationRepository(MAmailDBContext db, UserRepository userRepository, JWT jwt)
        {
            _db = db;
            _userRepository = userRepository;
            _JWT = jwt;
        }

        public async Task<RegisterResponse> Register(UserCreateRequestDto user)
        {
            var isEmailUsed = await _userRepository.GetUserByEmail(user.Email);

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

        public async Task<LoginResponse> Login(UserLoginDto user)
        {
            var userByEmail = await _userRepository.GetUserByEmail(user.Email);

            if (userByEmail == null || !PasswordSecurity.VerifyHashedPassword(userByEmail.PasswordHash, user.Password))
            {
                return new LoginResponse()
                {
                    Success = false,
                    Token = null,
                };
            }

            return new LoginResponse()
            {
                Success = true,
                Token = _JWT.GetToken(userByEmail)
            };
        }
    }
}
