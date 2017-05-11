using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomePageEF
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }

    public class UserRepository : IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DBAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserPassword == password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Email = user.UserEmail };
        }

        public UserModel Register(string email, string password)
        {
            if (DBAccessor.Instance.Users.Count
                (t => t.UserEmail.ToLower() == email.ToLower()) == 0)
            {
                var user = DBAccessor.Instance.Users
                        .Add(new User { UserEmail = email, UserPassword = password });

                DBAccessor.Instance.SaveChanges();

                return new UserModel { Id = user.UserId, Email = user.UserEmail };
            }
            else return null;
        }
    }
}
