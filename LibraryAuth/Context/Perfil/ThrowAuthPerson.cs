using LibraryAuth.Models.PerfilAuth;
using System;
using System.Collections.Generic;

namespace LibraryAuth.Context.PerfilAuth
{
    public class ThrowAuthPerson : INterfaceAuthPerson
    {
        public IEnumerable<PersonAuthLibrary> List()
        {
            throw new NotImplementedException();
        }
        public PersonAuthLibrary Get(int? Id)
        {
            throw new NotImplementedException();
        }
        public void Post(PersonAuthLibrary personAuthLibrary)
        {
            throw new NotImplementedException();
        }
        public void Put(PersonAuthLibrary personAuthLibrary, int? Id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int? Id)
        {
            throw new NotImplementedException();
        }
    }
}
