using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAuth.Api;
using WebAuth.Models.Perfil;

namespace WebAuth.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        protected readonly ApiClient _clientSchedule;

        public ScheduleController()
        {
            _clientSchedule = new ApiClient();
        }

        public ScheduleController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Schedule
        public async Task<ActionResult> Index()
        {
            var allSchedule = await _clientSchedule.GetSchedule();
            var allPeople = await _clientSchedule.GetPerson();
            var containerPersonSchedules = new List<PeopleSchedule>();

            if (allSchedule.IsSuccessStatusCode)
            {
                var schedules = await allSchedule.Content.ReadAsAsync<IEnumerable<Schedule>>();

                if (allPeople.IsSuccessStatusCode)
                {
                    var People = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();

                    foreach (var schedule in schedules)
                    {
                        foreach (var person in People)
                        {
                            if (schedule.PersonId.Equals(person.Id))
                            {
                                containerPersonSchedules.Add(new PeopleSchedule()
                                {
                                    People = person,
                                    Schedules = schedule,
                                    PeopleSelect = new List<SelectListItem>() {
                                        new SelectListItem()
                                        {
                                            Value = person.Id.ToString(),
                                            Text = $"{person.FirstName} {person.LastName}",
                                            Selected = person.Id.Equals(schedule.PersonId)
                                        }
                                    }
                                });
                            }
                        }
                    }
                    return View(containerPersonSchedules);
                }
            }
            return View(new List<PeopleSchedule>());
        }

        // GET: Schedule/Details/5
        public async Task<ActionResult> Details(int? Id)
        {
            var Schedules = await _clientSchedule.GetScheduleById(Id);
            var PersonSchedule = new PersonSchedule();

            if (Schedules.IsSuccessStatusCode)
            {
                var schedule = await Schedules.Content.ReadAsAsync<Schedule>();
                var people = await _clientSchedule.GetPersonById(schedule.PersonId);

                if (people.IsSuccessStatusCode)
                {
                    var person = await people.Content.ReadAsAsync<Person>();

                    if (schedule.Id.Equals(person.Id))
                    {
                        PersonSchedule = new PersonSchedule()
                        {
                            Person = person,
                            Schedule = schedule,
                            PersonScheduleSelect = new SelectListItem()
                            {
                                Value = person.Id.ToString(),
                                Text = $"{person.FirstName} {person.LastName}",
                                Selected = person.Id.Equals(schedule.PersonId)
                            }
                        };
                    }
                    return View(PersonSchedule);
                }
            }
            return View(new PersonSchedule());
        }

        // GET: Schedule/Create
        public async Task<ActionResult> Create()
        {
            var allPeople = await _clientSchedule.GetPerson();

            if (allPeople.IsSuccessStatusCode)
            {
                var people = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();
                var selectScheduleList = new List<SelectListItem>();
                var schedule = new Schedule();

                foreach (var person in people)
                {
                    var selectSchedule = new SelectListItem()
                    {
                        Value = person.Id.ToString(),
                        Text = $"Nome: {person.FirstName} {person.LastName}",
                        Selected = person.Id == schedule.PersonId
                    };
                    selectScheduleList.Add(selectSchedule);
                    schedule.PeopleSelect = selectScheduleList;
                }
                return View(schedule);
            }
            return View(new Schedule());
        }

        // POST: Schedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Schedule schedule)
        {
            try
            {
                // TODO: Add insert logic here
                await _clientSchedule.PostSchedule(schedule);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Schedule());
            }
        }

        // GET: Schedule/Edit/5
        public async Task<ActionResult> Edit(int? Id)
        {
            var allSchedule = await _clientSchedule.GetScheduleById(Id);
            var personSchedule = new PersonSchedule();

            if (allSchedule.IsSuccessStatusCode)
            {
                var schedule = await allSchedule.Content.ReadAsAsync<Schedule>();
                var allPeople = await _clientSchedule.GetPersonById(schedule.PersonId);

                if (allPeople.IsSuccessStatusCode)
                {
                    var person = await allPeople.Content.ReadAsAsync<Person>();

                    personSchedule = new PersonSchedule()
                    {
                        Schedule = schedule,
                        Person = person,
                        PeopleSelect = new List<SelectListItem>()
                        {
                            new SelectListItem()
                            {
                                Value = person.Id.ToString(),
                                Text = $"{person.FirstName} {person.LastName}",
                                Selected = person.Id.Equals(schedule.PersonId)
                            }
                        }
                    };
                }
                return View(personSchedule);
            }
            return View(new PersonSchedule());
        }

        // POST: Schedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Schedule schedule, int? Id)
        {
            try
            {
                // TODO: Add insert logic here
                _ = _clientSchedule.PutSchedule(schedule, Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Schedule());
            }
        }

        // GET: Schedule/Delete/5
        public async Task<ActionResult> Delete(int? Id)
        {
            var schedules = await _clientSchedule.GetScheduleById(Id);

            if (schedules.IsSuccessStatusCode)
            {
                var schedule = await schedules.Content.ReadAsAsync<Schedule>();
                return View(schedule);
            }
            return View(new Schedule());
        }

        // POST: Schedule/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var schedules = await _clientSchedule.DeleteSchedule(Id);

            try
            {
                // TODO: Add delete logic here
                if (schedules.IsSuccessStatusCode)
                {
                    await schedules.Content.ReadAsAsync<Schedule>();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Schedule());
            }
        }
    }
}