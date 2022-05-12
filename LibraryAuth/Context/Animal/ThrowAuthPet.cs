using LibraryAuth.Models.AnimalAuth;
using System;
using System.Collections.Generic;

namespace LibraryAuth.Context.Animal
{
    public class ThrowAuthPet : INterfaceAuthPet
    {
        public IEnumerable<PetAuthLibrary> List()
        {
            throw new NotImplementedException();
        }
        public PetAuthLibrary Get(int? Id)
        {
            throw new NotImplementedException();
        }
        public void Post(PetAuthLibrary petLibrary)
        {
            throw new NotImplementedException();
        }
        public void Put(PetAuthLibrary petLibrary, int? Id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int? Id)
        {
            throw new NotImplementedException();
        }
    }
}
