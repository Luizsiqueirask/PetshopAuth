using LibraryAuth.Models.PerfilAuth;
using System.Collections.Generic;

namespace LibraryAuth.Context.PerfilAuth.User
{
    public interface INterfaceUser
    {
        IEnumerable<UserAuthLibrary> List();
        UserAuthLibrary Get(int? Id);
        void Post(UserAuthLibrary userAuthLibrary);
        void Put(UserAuthLibrary userAuthLibrary, int? Id);
        void Delete(int? Id);
    }
}
