using System;

namespace PerfilAuth.Models.PerfilAuth
{
    public class PersonAuth
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }
        public DateTime Birthday { get; set; }
        public PictureAuth Picture { get; set; }
        public UserAuth User { get; set; }
        public ContactAuth Contact { get; set; }
        public AddressAuth Address { get; set; }
    }
}
