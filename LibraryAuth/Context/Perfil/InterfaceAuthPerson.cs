using LibraryAuth.Models.PerfilAuth;
using System.Collections.Generic;

namespace LibraryAuth.Context.PerfilAuth
{
    public interface INterfaceAuthPerson
    {
        IEnumerable<PersonAuthLibrary> List();
        PersonAuthLibrary Get(int? Id);
        void Post(PersonAuthLibrary personAuthLibrary);
        void Put(PersonAuthLibrary personAuthLibrary, int? Id);
        void Delete(int? Id);
    }
}
