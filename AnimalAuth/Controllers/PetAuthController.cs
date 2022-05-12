using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Animal.Controllers
{
    public class PetAuthController : ApiController
    {
        private readonly PetCast petCast;

        public PetAuthController()
        {
            petCast = new PetCast();
        }

        // GET: api/Pet
        public IEnumerable<Pet> Get()
        {
            return petCast.List();
        }

        // GET: api/Pet/5
        public Pet Get(int? Id)
        {
            return petCast.Get(Id);
        }

        // POST: api/Pet
        public string Post(Pet pet)
        {
            try
            {
                if (pet != null)
                {
                    var httpResponseOk = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Sucesso"),
                        RequestMessage = new HttpRequestMessage(),
                    };
                    petCast.Post(pet);
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
        public void Put(Pet pet, int? Id)
        {
            petCast.Put(pet, Id);
        }

        // DELETE: api/Pet/5
        public void Delete(int? Id)
        {
            petCast.Delete(Id);
        }
    }
}
