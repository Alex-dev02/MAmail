using MAmail.Data;
using MAmail.Entities;

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
    }
}
