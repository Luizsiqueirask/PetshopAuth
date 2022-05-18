using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebAuth.Models.PerfilAuth;

namespace WebAuth.Models.Perfil
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe nome")]
        [DisplayName("Nome")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Informe sobrenome")]
        [DisplayName("Sobrenome")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Informe idade")]
        [DisplayName("Idade")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Inserido Genero")]
        [DisplayName("Genero")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Informe aniversário")]
        [DisplayName("Data de aniversário")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, HtmlEncode = true)]
        public DateTime Birthday { get; set; }
        public Picture Picture { get; set; }
        public User User { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
    }
}
