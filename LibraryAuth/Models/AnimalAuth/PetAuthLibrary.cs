using System;

namespace LibraryAuth.Models.AnimalAuth
{
    public class PetAuthLibrary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public int Age { get; set; }
        public int PersonId { get; set; }
        public DateTime Birthday { get; set; }
        public ImageAuthLibrary Image { get; set; }
        public HealthAuthLibrary Health { get; set; }
    }
}
