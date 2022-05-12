using LibraryAuth.Context.PerfilAuth.User;
using LibraryAuth.Models.PerfilAuth;
using PerfilAuth.Models.PerfilAuth;
using System.Collections.Generic;

namespace PerfilAuth.Casting
{
    public class UserCastAuth
    {
        private readonly ClassAuthUser classAuthUser;
        public UserCastAuth()
        {
            classAuthUser = new ClassAuthUser();
        }

        public IEnumerable<UserAuth> List()
        {
            var listUser = new List<UserAuth>();
            var allUser = classAuthUser.List();

            if (allUser != null)
            {
                foreach (var user in allUser)
                {
                    listUser.Add(new UserAuth()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Password = user.Password
                    });
                }
                return listUser;
            }
            return new List<UserAuth>();
        }
        public UserAuth Get(int? Id)
        {
            var users = classAuthUser.Get(Id);

            if (users != null)
            {
                var user = new UserAuth()
                {
                    Id = users.Id,
                    Username = users.Username,
                    Password = users.Password
                };
                return user;
            }
            return new UserAuth();
        }
        public void Post(UserAuth userAuth)
        {
            if (userAuth != null)
            {
                var userAuthLibrary = new UserAuthLibrary()
                {
                    Id = userAuth.Id,
                    Username = userAuth.Username,
                    Password = userAuth.Password
                };

                classAuthUser.Post(userAuthLibrary);
            }
        }
        public void Put(UserAuth userAuth, int? Id)
        {
            if (userAuth != null)
            {
                var userAuthLibrary = new UserAuthLibrary()
                {
                    Id = userAuth.Id,
                    Username = userAuth.Username,
                    Password = userAuth.Password
                };

                classAuthUser.Put(userAuthLibrary, Id);
            }
        }
        public void Delete(int? Id)
        {
            classAuthUser.Delete(Id);
        }
    }
}