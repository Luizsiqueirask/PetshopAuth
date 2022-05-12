using System;

namespace LibraryAuth.Models.PerfilAuth
{
    public class PersonAuthLibrary
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }
        public DateTime Birthday { get; set; }
        public PictureAuthLibrary Picture { get; set; }
        public UserAuthLibrary User { get; set; }
        public ContactAuthLibrary Contact { get; set; }
        public AddressAuthLibrary Address { get; set; }
    }
}
