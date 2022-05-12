using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAuth.Models.Perfil
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a tipo de serviço")]
        [DisplayName("Serviço para seu animal")]
        public string Services { get; set; }

        [Required(ErrorMessage = "Informe a data do agendamento")]
        [DisplayName("Data do agendamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, HtmlEncode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Informe a hora do agendamento")]
        [DisplayName("Hora do Agendamento")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh:mm}", ApplyFormatInEditMode = true, HtmlEncode = false)]
        public DateTime Time { get; set; }
        public int PersonId { get; set; }

    }
}
