using PerfilAuth.Casting;
using PerfilAuth.Models.PerfilAuth;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PerfilAuth.Controllers
{
    public class PersonController : ApiController
    {
        private readonly PersonCastAuth personCastAuth;
        public PersonController()
        {
            personCastAuth = new PersonCastAuth();
        }

        // GET: api/Person
        public IEnumerable<PersonAuth> Get()
        {
            return personCastAuth.List();
        }

        // GET: api/Person/5
        public PersonAuth Get(int? Id)
        {
            return personCastAuth.Get(Id);
        }

        // POST: api/Person
        public string Post(PersonAuth person)
        {
            try
            {
                if (person != null)
                {
                    var httpResponseOk = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Sucesso"),
                        RequestMessage = new HttpRequestMessage(),
                    };
                    personCastAuth.Post(person);
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

        // PUT: api/Person/5
        public void Put(PersonAuth person, int? Id)
        {
            personCastAuth.Put(person, Id);
        }

        // DELETE: api/Person/5
        public void Delete(int? Id)
        {
            personCastAuth.Delete(Id);
        }
    }
}
