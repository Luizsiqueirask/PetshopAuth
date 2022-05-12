using LibraryAuth.Models.PerfilAuth;
using System;
using System.Collections.Generic;

namespace LibraryAuth.Context.PerfilAuth.Schedule
{
    public class ThrowAuthSchedule : INterfaceAuthSchedule
    {
        public IEnumerable<ScheduleAuthLibrary> List()
        {
            throw new NotImplementedException();
        }
        public ScheduleAuthLibrary Get(int? Id)
        {
            throw new NotImplementedException();
        }
        public void Post(ScheduleAuthLibrary scheduleLibrary)
        {
            throw new NotImplementedException();
        }
        public void Put(ScheduleAuthLibrary scheduleLibrary, int? Id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int? Id)
        {
            throw new NotImplementedException();
        }
    }
}
