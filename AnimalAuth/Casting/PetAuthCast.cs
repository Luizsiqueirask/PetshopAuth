using AnimalAuth.Models.AnimalAuth;
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

        public IEnumerable<PetAuth> List()
        {
            var listPet = new List<PetAuth>();
            var allPet = classAuthPet.List();

            if (allPet != null)
            {
                foreach (var pets in allPet)
                {
                    listPet.Add(new PetAuth()
                    {
                        Id = pets.Id,
                        Name = pets.Name,
                        Age = pets.Age,
                        Birthday = pets.Birthday,
                        Genre = pets.Genre,
                        Type = pets.Type,
                        PersonId = pets.PersonId,
                        Image = new ImageAuth()
                        {
                            Id = pets.Image.Id,
                            Tag = pets.Image.Tag,
                            Path = pets.Image.Path
                        }
                    });
                }
                return listPet;
            }
            return new List<PetAuth>();
        }
        public PetAuth Get(int? Id)
        {
            var pets = classAuthPet.Get(Id);

            if (pets != null)
            {
                var petAuth = new PetAuth()
                {
                    Id = pets.Id,
                    Name = pets.Name,
                    Age = pets.Age,
                    Birthday = pets.Birthday,
                    Genre = pets.Genre,
                    Type = pets.Type,
                    PersonId = pets.PersonId,
                    Image = new ImageAuth()
                    {
                        Id = pets.Image.Id,
                        Tag = pets.Image.Tag,
                        Path = pets.Image.Path
                    }
                };
                return petAuth;
            }
            return new PetAuth();
        }
        public void Post(PetAuth petAuth)
        {
            var petLibrary = new PetAuthLibrary()
            {
                Id = petAuth.Id,
                Name = petAuth.Name,
                Age = petAuth.Age,
                Birthday = petAuth.Birthday,
                Genre = petAuth.Genre,
                Type = petAuth.Type,
                PersonId = petAuth.PersonId,
                Image = new ImageAuthLibrary()
                {
                    Id = petAuth.Image.Id,
                    Tag = petAuth.Image.Tag,
                    Path = petAuth.Image.Path
                },
                Health = new HealthAuthLibrary()
                {
                    Id = petAuth.Health.Id,
                    Status = petAuth.Health.Status
                }
            };

            classAuthPet.Post(petLibrary);
        }
        public void Put(PetAuth petAuth, int? Id)
        {
            var petLibrary = new PetAuthLibrary()
            {
                Id = petAuth.Id,
                Name = petAuth.Name,
                Age = petAuth.Age,
                Birthday = petAuth.Birthday,
                Genre = petAuth.Genre,
                Type = petAuth.Type,
                PersonId = petAuth.PersonId,
                Image = new ImageAuthLibrary()
                {
                    Id = petAuth.Image.Id,
                    Tag = petAuth.Image.Tag,
                    Path = petAuth.Image.Path
                },
                Health = new HealthAuthLibrary()
                {
                    Id = petAuth.Health.Id,
                    Status = petAuth.Health.Status
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