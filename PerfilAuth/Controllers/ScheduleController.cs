using PerfilAuth.Casting;
using PerfilAuth.Models.PerfilAuth;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PerfilAuth.Controllers
{
    public class ScheduleController : ApiController
    {
        private readonly ScheduleCastAuth scheduleCastAuth;

        public ScheduleController()
        {
            scheduleCastAuth = new ScheduleCastAuth();
        }

        // GET: api/Schedule
        public IEnumerable<ScheduleAuth> Get()
        {
            return scheduleCastAuth.List();
        }

        // GET: api/Schedule/5
        public ScheduleAuth Get(int? Id)
        {
            return scheduleCastAuth.Get(Id);
        }

        // POST: api/Schedule
        public string Post(ScheduleAuth scheduleAuth)
        {
            try
            {
                if (scheduleAuth != null)
                {
                    var httpResponseOk = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Sucesso"),
                        RequestMessage = new HttpRequestMessage(),
                    };
                    scheduleCastAuth.Post(scheduleAuth);
                    return httpResponseOk.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            var httpResponseBad = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Error"),
                RequestMessage = new HttpRequestMessage(),
            };
            return httpResponseBad.ToString();
        }

        // PUT: api/Schedule/5
        public void Put(ScheduleAuth scheduleAuth, int? Id)
        {
            scheduleCastAuth.Put(scheduleAuth, Id);
        }

        // DELETE: api/Schedule/5
        public void Delete(int? Id)
        {
            scheduleCastAuth.Delete(Id);
        }
    }
}
