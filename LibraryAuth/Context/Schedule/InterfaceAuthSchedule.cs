using LibraryAuth.Models.PerfilAuth;
using System.Collections.Generic;

namespace LibraryAuth.Context.PerfilAuth
{
    public interface INterfaceAuthSchedule
    {
        IEnumerable<ScheduleAuthLibrary> List();
        ScheduleAuthLibrary Get(int? Id);
        void Post(ScheduleAuthLibrary scheduleLibrary);
        void Put(ScheduleAuthLibrary scheduleLibrary, int? Id);
        void Delete(int? Id);
    }
}
