using LibraryAuth.Context.PerfilAuth;
using LibraryAuth.Models.PerfilAuth;
using PerfilAuth.Models.PerfilAuth;
using System.Collections.Generic;

namespace PerfilAuth.Casting
{
    public class PersonCastAuth
    {
        private readonly ClassAuthPerson classAuthPerson;
        public PersonCastAuth()
        {
            classAuthPerson = new ClassAuthPerson();
        }

        public IEnumerable<PersonAuth> List()
        {
            var listPerson = new List<PersonAuth>();
            var allPerson = classAuthPerson.List();

            if (allPerson != null)
            {
                foreach (var people in allPerson)
                {
                    listPerson.Add(new PersonAuth()
                    {
                        Id = people.Id,
                        FirstName = people.FirstName,
                        LastName = people.LastName,
                        Age = people.Age,
                        Birthday = people.Birthday,
                        Genre = people.Genre,
                        Picture = new PictureAuth()
                        {
                            Id = people.Picture.Id,
                            Tag = people.Picture.Tag,
                            Path = people.Picture.Path
                        },                      
                        Contact = new ContactAuth()
                        {
                            Id = people.Contact.Id,
                            Email = people.Contact.Email,
                            Mobile = people.Contact.Mobile
                        },
                        Address = new AddressAuth()
                        {
                            Id = people.Address.Id,
                            Country = people.Address.Country,
                            States = people.Address.States,
                            City = people.Address.City,
                            Neighborhoods = people.Address.Neighborhoods
                        }
                    });
                }
                return listPerson;
            }
            return new List<PersonAuth>();
        }
        public PersonAuth Get(int? Id)
        {
            var people = classAuthPerson.Get(Id);

            if (people != null)
            {
                var person = new PersonAuth()
                {
                    Id = people.Id,
                    FirstName = people.FirstName,
                    LastName = people.LastName,
                    Age = people.Age,
                    Birthday = people.Birthday,
                    Genre = people.Genre,
                    Picture = new PictureAuth()
                    {
                        Id = people.Picture.Id,
                        Tag = people.Picture.Tag,
                        Path = people.Picture.Path
                    },
                    Contact = new ContactAuth()
                    {
                        Id = people.Contact.Id,
                        Email = people.Contact.Email,
                        Mobile = people.Contact.Mobile
                    },
                    Address = new AddressAuth()
                    {
                        Id = people.Address.Id,
                        Country = people.Address.Country,
                        States = people.Address.States,
                        City = people.Address.City,
                        Neighborhoods = people.Address.Neighborhoods
                    }
                };
                return person;
            }
            return new PersonAuth();
        }
        public void Post(PersonAuth person)
        {
            if (person != null)
            {
                var personLibrary = new PersonAuthLibrary()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Birthday = person.Birthday,
                    Age = person.Age,
                    Genre = person.Genre,
                    Picture = new PictureAuthLibrary()
                    {
                        Id = person.Picture.Id,
                        Tag = person.Picture.Tag,
                        Path = person.Picture.Path
                    },
                    Contact = new ContactAuthLibrary()
                    {
                        Id = person.Contact.Id,
                        Email = person.Contact.Email,
                        Mobile = person.Contact.Mobile
                    },
                    Address = new AddressAuthLibrary()
                    {
                        Id = person.Address.Id,
                        Country = person.Address.Country,
                        States = person.Address.States,
                        City = person.Address.City,
                        Neighborhoods = person.Address.Neighborhoods
                    }
                };
                classAuthPerson.Post(personLibrary);
            }
        }
        public void Put(PersonAuth person, int? Id)
        {
            if (person != null)
            {
                var personLibrary = new PersonAuthLibrary()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Birthday = person.Birthday,
                    Age = person.Age,
                    Genre = person.Genre,

                    Picture = new PictureAuthLibrary()
                    {
                        Id = person.Picture.Id,
                        Tag = person.Picture.Tag,
                        Path = person.Picture.Path
                    },
                    Contact = new ContactAuthLibrary()
                    {
                        Id = person.Contact.Id,
                        Email = person.Contact.Email,
                        Mobile = person.Contact.Mobile
                    },
                    Address = new AddressAuthLibrary()
                    {
                        Id = person.Address.Id,
                        Country = person.Address.Country,
                        States = person.Address.States,
                        City = person.Address.City,
                        Neighborhoods = person.Address.Neighborhoods
                    }
                };
                classAuthPerson.Put(personLibrary, Id);
            }
        }
        public void Delete(int? Id)
        {
            classAuthPerson.Delete(Id);
        }
    }
}