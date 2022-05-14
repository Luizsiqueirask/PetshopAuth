using AnimalAuth.Casting;
using AnimalAuth.Models.AnimalAuth;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Animal.Controllers
{
    public class PetAuthController : ApiController
    {
        private readonly PetAuthCast petAuthCast;

        public PetAuthController()
        {
            petAuthCast = new PetAuthCast();
        }

        // GET: api/Pet
        public IEnumerable<PetAuth> Get()
        {
            return petAuthCast.List();
        }

        // GET: api/Pet/5
        public PetAuth Get(int? Id)
        {
            return petAuthCast.Get(Id);
        }

        // POST: api/Pet
        public string Post(PetAuth petAuth)
        {
            try
            {
                if (petAuth != null)
                {
                    var httpResponseOk = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Sucesso"),
                        RequestMessage = new HttpRequestMessage(),
                    };
                    petAuthCast.Post(petAuth);
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

        // PUT: api/Pet/5
        public void Put(PetAuth petAuth, int? Id)
        {
            petAuthCast.Put(petAuth, Id);
        }

        // DELETE: api/Pet/5
        public void Delete(int? Id)
        {
            petAuthCast.Delete(Id);
        }
    }
}
