using System;

namespace AnimalAuth.Models.AnimalAuth
{
    public class PetAuth
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public ImageAuth Image { get; set; }
        public HealthAuth Health { get; set; }
        public int PersonId { get; set; }
    }
}
