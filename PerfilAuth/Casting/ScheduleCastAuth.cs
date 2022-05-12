using LibraryAuth.Context.PerfilAuth.Schedule;
using LibraryAuth.Models.PerfilAuth;
using PerfilAuth.Models.PerfilAuth;
using System.Collections.Generic;

namespace PerfilAuth.Casting
{
    public class ScheduleCastAuth
    {
        private readonly ClassAuthSchedule classAuthSchedule;
        public ScheduleCastAuth()
        {
            classAuthSchedule = new ClassAuthSchedule();
        }

        public IEnumerable<ScheduleAuth> List()
        {
            var listSchedule = new List<ScheduleAuth>();
            var allSchedule = classAuthSchedule.List();

            if (allSchedule != null)
            {
                foreach (var schedule in allSchedule)
                {
                    listSchedule.Add(new ScheduleAuth()
                    {
                        Id = schedule.Id,
                        Services = schedule.Services,
                        Date = schedule.Date,
                        Time = schedule.Time,
                        PersonId = schedule.PersonId
                    });
                }
                return listSchedule;
            }
            return new List<ScheduleAuth>();
        }
        public ScheduleAuth Get(int? Id)
        {
            var schedules = classAuthSchedule.Get(Id);

            if (schedules != null)
            {
                var schedule = new ScheduleAuth()
                {
                    Id = schedules.Id,
                    Services = schedules.Services,
                    Date = schedules.Date,
                    Time = schedules.Time,
                    PersonId = schedules.PersonId
                };
                return schedule;
            }
            return new ScheduleAuth();
        }
        public void Post(ScheduleAuth scheduleAuth)
        {
            if (scheduleAuth != null)
            {
                var scheduleAuthLibrary = new ScheduleAuthLibrary()
                {
                    Id = scheduleAuth.Id,
                    Services = scheduleAuth.Services,
                    Date = scheduleAuth.Date,
                    Time = scheduleAuth.Time,
                    PersonId = scheduleAuth.PersonId
                };

                classAuthSchedule.Post(scheduleAuthLibrary);
            }
        }
        public void Put(ScheduleAuth scheduleAuth, int? Id)
        {
            if (scheduleAuth != null)
            {
                var scheduleAuthLibrary = new ScheduleAuthLibrary()
                {
                    Id = scheduleAuth.Id,
                    Services = scheduleAuth.Services,
                    Date = scheduleAuth.Date,
                    Time = scheduleAuth.Time,
                    PersonId = scheduleAuth.PersonId
                };
                classAuthSchedule.Put(scheduleAuthLibrary, Id);
            }
        }
        public void Delete(int? Id)
        {
            classAuthSchedule.Delete(Id);
        }
    }
}