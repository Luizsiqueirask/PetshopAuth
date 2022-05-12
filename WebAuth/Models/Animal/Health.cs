using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAuth.Models.Animal
{
    public class Health
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Inserido estado de saúde do seu animal")]
        [DisplayName("Estado de saúde")]
        public string Status { get; set; }
    }
}
