using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebAuth.Models.Perfil;

namespace WebAuth.Models.Animal
{
    public class Pet
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe nome")]
        [DisplayName("Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Informe tipo de animal de estimação. [Gato, Cão, e etc...]")]
        [DisplayName("Tipo")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Informe a idade Pet")]
        [DisplayName("Idade")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Inserido Gênero")]
        [DisplayName("Gênero")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Informe aniversário do seu Pet")]
        [DisplayName("Data de aniversário")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, HtmlEncode = true)]
        public DateTime Birthday { get; set; }
        public Image Image { get; set; }
        public Health Health { get; set; }
        //public Schedule Schedule { get; set; }

        [Required(ErrorMessage = "Informe o nome do dono do animal")]
        [DisplayName("Dono do animal")]
        public int PersonId { get; set; }
        public SelectListItem PersonSelect { get; set; }
        public IEnumerable<SelectListItem> PeopleSelect { get; set; }
    }
    public class PersonPet
    {
        public Person Person { get; set; }
        public Pet Pet { get; set; }
        public SelectListItem PersonPetsSelect { get; set; }
    }
    public class PeoplePets
    {
        public Person People { get; set; }
        public Pet Pets { get; set; }
        public SelectListItem PersonSelect { get; set; }
        public IEnumerable<SelectListItem> PeopleSelect { get; set; }
    }
}
