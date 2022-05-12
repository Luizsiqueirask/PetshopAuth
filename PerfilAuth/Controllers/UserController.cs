using PerfilAuth.Casting;
using PerfilAuth.Models.PerfilAuth;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PerfilAuth.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserCastAuth userCastAuth;

        public UserController()
        {
            userCastAuth = new UserCastAuth();
        }

        // GET: api/User
        public IEnumerable<UserAuth> Get()
        {
            return userCastAuth.List();
        }

        // GET: api/User/5
        public UserAuth Get(int? Id)
        {
            return userCastAuth.Get(Id);
        }

        // POST: api/User
        public string Post(UserAuth userAuth)
        {
            try
            {
                if (userAuth != null)
                {
                    var httpResponseOk = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Sucesso"),
                        RequestMessage = new HttpRequestMessage(),
                    };
                    userCastAuth.Post(userAuth);
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

        // PUT: api/User/5
        public void Put(UserAuth userAuth, int? Id)
        {
            userCastAuth.Put(userAuth, Id);
        }

        // DELETE: api/User/5
        public void Delete(int Id)
        {
            userCastAuth.Delete(Id);
        }
    }
}
