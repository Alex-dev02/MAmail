using MAmail.Data;
using MAmail.Dtos;
using MAmail.Entities;
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
        public User? GetUserById(int userId)
        {
            return _db.Users.Find(userId);
        }
        public void UpdateUser(UserUpdateDto updatedUser, User user)
        {
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;

            _db.SaveChanges();
        }
        public void DeleteUser(User user)
        {
            _db.Users.Remove(user);

            _db.SaveChanges();
        }
    }
}
