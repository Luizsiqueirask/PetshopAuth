using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebAuth.Models.Perfil
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a tipo de serviço")]
        [DisplayName("Serviço")]
        public string Services { get; set; }

        [Required(ErrorMessage = "Informe a data do agendamento")]
        [DisplayName("Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, HtmlEncode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Informe a hora do agendamento")]
        [DisplayName("Hora")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = @"{0:hh:mm}", ApplyFormatInEditMode = true, HtmlEncode = true)]
        public DateTime Time { get; set; }
        [DisplayName("Dono")]
        public int PersonId { get; set; }
        public SelectListItem PersonSelect { get; set; }
        public IEnumerable<SelectListItem> PeopleSelect { get; set; }
    }

    public class PersonSchedule
    {
        public Person Person { get; set; }
        public Schedule Schedule { get; set; }
        public SelectListItem PersonScheduleSelect { get; set; }
        public IEnumerable<SelectListItem> PeopleSelect { get; set; }
    }
    public class PeopleSchedule
    {
        public Person People { get; set; }
        public Schedule Schedules { get; set; }
        public SelectListItem PersonSelect { get; set; }
        public IEnumerable<SelectListItem> PeopleSelect { get; set; }
    }
}
