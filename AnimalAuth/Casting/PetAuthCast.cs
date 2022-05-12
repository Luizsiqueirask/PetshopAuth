using LibraryAuth.Context.Animal;
using LibraryAuth.Models.AnimalAuth;

using System.Collections.Generic;

namespace AnimalAuth.Casting
{
    public class PetAuthCast
    {
        private readonly ClassAuthPet classAuthPet;
        public PetAuthCast()
        {
            classAuthPet = new ClassAuthPet();
        }

        public IEnumerable<PetAuthLibrary> List()
        {
            var listPetLibrary = new List<PetAuthLibrary>();
            var allPetLibrary = classAuthPet.List();

            if (allPetLibrary != null)
            {
                foreach (var pets in allPetLibrary)
                {
                    listPetLibrary.Add(new PetAuthLibrary()
                    {
                        Id = pets.Id,
                        Name = pets.Name,
                        Age = pets.Age,
                        Birthday = pets.Birthday,
                        Genre = pets.Genre,
                        Type = pets.Type,
                        PersonId = pets.PersonId,
                        Image = new ImageAuthLibrary()
                        {
                            Id = pets.Image.Id,
                            Tag = pets.Image.Tag,
                            Path = pets.Image.Path
                        }
                    });
                }
                return listPetLibrary;
            }
            return new List<PetAuthLibrary>();
        }
        public PetAuthLibrary Get(int? Id)
        {
            var pets = classAuthPet.Get(Id);

            if (pets != null)
            {
                var petAuthLibrary = new PetAuthLibrary()
                {
                    Id = pets.Id,
                    Name = pets.Name,
                    Age = pets.Age,
                    Birthday = pets.Birthday,
                    Genre = pets.Genre,
                    Type = pets.Type,
                    PersonId = pets.PersonId,
                    Image = new ImageAuthLibrary()
                    {
                        Id = pets.Image.Id,
                        Tag = pets.Image.Tag,
                        Path = pets.Image.Path
                    }
                };
                return petAuthLibrary;
            }
            return new PetAuthLibrary();
        }
        public void Post(PetAuthLibrary petAuthLibrary)
        {
            var petLibrary = new PetAuthLibrary()
            {
                Id = petAuthLibrary.Id,
                Name = petAuthLibrary.Name,
                Age = petAuthLibrary.Age,
                Birthday = petAuthLibrary.Birthday,
                Genre = petAuthLibrary.Genre,
                Type = petAuthLibrary.Type,
                PersonId = petAuthLibrary.PersonId,
                Image = new ImageAuthLibrary()
                {
                    Id = petAuthLibrary.Image.Id,
                    Tag = petAuthLibrary.Image.Tag,
                    Path = petAuthLibrary.Image.Path
                },
                Health = new HealthAuthLibrary()
                {
                    Id = petAuthLibrary.Health.Id,
                    Status = petAuthLibrary.Health.Status
                }
            };

            classAuthPet.Post(petLibrary);
        }
        public void Put(PetAuthLibrary petAuthLibrary, int? Id)
        {
            var petLibrary = new PetAuthLibrary()
            {
                Id = petAuthLibrary.Id,
                Name = petAuthLibrary.Name,
                Age = petAuthLibrary.Age,
                Birthday = petAuthLibrary.Birthday,
                Genre = petAuthLibrary.Genre,
                Type = petAuthLibrary.Type,
                PersonId = petAuthLibrary.PersonId,
                Image = new ImageAuthLibrary()
                {
                    Id = petAuthLibrary.Image.Id,
                    Tag = petAuthLibrary.Image.Tag,
                    Path = petAuthLibrary.Image.Path
                },
                Health = new HealthAuthLibrary()
                {
                    Id = petAuthLibrary.Health.Id,
                    Status = petAuthLibrary.Health.Status
                }
            };

            classAuthPet.Put(petLibrary, Id);
        }
        public void Delete(int? Id)
        {
            classAuthPet.Delete(Id);
        }
    }
}