using System;

namespace LibraryAuth.Models.PerfilAuth
{
    public class ScheduleAuthLibrary
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int PersonId { get; set; }
    }
}
