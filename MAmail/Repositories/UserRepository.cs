using MAmail.Data;
using MAmail.Dtos;
using MAmail.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MAmail.Repositories
{
    public class UserRepository
    {
        private MAmailDBContext _db;

        public UserRepository(MAmailDBContext db)
        {
            _db = db;
        }

        public void CreateUser(User user)
        {
            _db.Users.Add(user);

            _db.SaveChanges();
        }
        public async Task<User?> GetUserById(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async void UpdateUser(UserUpdateDto updatedUser, User user)
        {
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;

            await _db.SaveChangesAsync();
        }

        public void DeleteUser(User user)
        {
            _db.Users.Remove(user);

            _db.SaveChanges();
        }
    }
}
