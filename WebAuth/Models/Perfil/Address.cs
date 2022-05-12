using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAuth.Models.Perfil
{
    public class Address
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe pais")]
        [DisplayName("Pais")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Informe Estado")]
        [DisplayName("Estado")]
        public string States { get; set; }
        [Required(ErrorMessage = "Informe cidade")]
        [DisplayName("Cidade")]
        public string City { get; set; }
        [Required(ErrorMessage = "Informe Bairro")]
        [DisplayName("Bairro")]
        public string Neighborhoods { get; set; }
    }
}
