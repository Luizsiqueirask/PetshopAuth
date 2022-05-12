using LibraryAuth.Models.PerfilAuth;
using System;
using System.Collections.Generic;

namespace LibraryAuth.Context.PerfilAuth.User
{
    public class ThrowAuthUser : INterfaceUser
    {
        public IEnumerable<UserAuthLibrary> List()
        {
            throw new NotImplementedException();
        }
        public UserAuthLibrary Get(int? Id)
        {
            throw new NotImplementedException();
        }
        public void Post(UserAuthLibrary userAuthLibrary)
        {
            throw new NotImplementedException();
        }
        public void Put(UserAuthLibrary userAuthLibrary, int? Id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int? Id)
        {
            throw new NotImplementedException();
        }
    }
}
