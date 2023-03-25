using MAmail.Data;
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
        public User? GetById(int userId)
        {
            return db.Users.Find(userId);
        }
    }
}
