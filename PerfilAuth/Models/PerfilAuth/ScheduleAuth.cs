using System;

namespace PerfilAuth.Models.PerfilAuth
{
    public class ScheduleAuth
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int PersonId { get; set; }
    }
}