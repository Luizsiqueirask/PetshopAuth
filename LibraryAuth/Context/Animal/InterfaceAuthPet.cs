using LibraryAuth.Models.AnimalAuth;
using System.Collections.Generic;

namespace LibraryAuth.Context.Animal
{
    public interface INterfaceAuthPet
    {
        IEnumerable<PetAuthLibrary> List();
        PetAuthLibrary Get(int? Id);
        void Post(PetAuthLibrary petLibrary);
        void Put(PetAuthLibrary petLibrary, int? Id);
        void Delete(int? Id);
    }
}
