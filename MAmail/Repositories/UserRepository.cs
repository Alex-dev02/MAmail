using MAmail.Data;
using MAmail.Dtos;
using MAmail.Entities;
using System.Linq;

namespace MAmail.Repositories
{
    public class UserRepository
    {
        private MAmailDBContext db;

        public UserRepository(MAmailDBContext db)
        {
            this.db = db;
        }

        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public User? GetUserById(int userId)
        {
            return db.Users.Find(userId);
        }
        public void UpdateUser(UserUpdateDto updatedUser, User user)
        {
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;

            db.SaveChanges();
        }
    }
}
