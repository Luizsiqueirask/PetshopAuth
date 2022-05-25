using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAuth.Models.Animal;
using WebAuth.Models.Perfil;
using WebAuth.Models.PerfilAuth;

namespace WebAuth.Api
{
    public class ApiClient
    {
        public readonly HttpClient _clientAnimal;
        public readonly HttpClient _clientPerfil;

        public ApiClient()
        {
            try
            {
                var apiUrl = new List<string>()
                {
                    "http://api-perfilauth-azure.azurewebsites.net/",
                    "http://api-animalauth-azure.azurewebsites.net/"
                };

                _clientPerfil = new HttpClient
                {
                    BaseAddress = new Uri(apiUrl[0])
                };

                _clientAnimal = new HttpClient
                {
                    BaseAddress = new Uri(apiUrl[1])
                };
            }
            catch
            {
                var ports = new List<int>() { 56444, 50584 };

                _clientPerfil = new HttpClient
                {
                    BaseAddress = new Uri($"http://localhost:{ports[0]}/")
                };

                _clientAnimal = new HttpClient
                {
                    BaseAddress = new Uri($"http://localhost:{ports[1]}/")
                };
            }

            _clientPerfil.DefaultRequestHeaders.Accept.Clear();
            _clientAnimal.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");

            _clientPerfil.DefaultRequestHeaders.Accept.Add(mediaType);
            _clientAnimal.DefaultRequestHeaders.Accept.Add(mediaType);

        }

        #region User
        public async Task<HttpResponseMessage> GetUser()
        {
            return await _clientPerfil.GetAsync("api/User");
        }
        public async Task<HttpResponseMessage> GetUserById(int? Id)
        {
            return await _clientPerfil.GetAsync($"api/User/{Id}");
        }
        public async Task<HttpResponseMessage> PostUser(User user)
        {
            return await _clientPerfil.PostAsJsonAsync("api/User", user);
        }
        public async Task<HttpResponseMessage> PutUser(User user, int? Id)
        {
            return await _clientPerfil.PutAsJsonAsync($"api/User/{Id}", user);
        }
        public async Task<HttpResponseMessage> DeleteUser(int? Id)
        {
            return await _clientPerfil.DeleteAsync($"api/User/{Id}");
        }
        #endregion User

        #region Person
        public async Task<HttpResponseMessage> GetPerson()
        {
            return await _clientPerfil.GetAsync("api/Person");
        }
        public async Task<HttpResponseMessage> GetPersonById(int? Id)
        {
            return await _clientPerfil.GetAsync($"api/Person/{Id}");
        }
        public async Task<HttpResponseMessage> PostPerson(Person person)
        {
            return await _clientPerfil.PostAsJsonAsync("api/Person", person);
        }
        public async Task<HttpResponseMessage> PutPerson(Person person, int? Id)
        {
            return await _clientPerfil.PutAsJsonAsync($"api/Person/{Id}", person);
        }
        public async Task<HttpResponseMessage> DeletePerson(int? Id)
        {
            return await _clientPerfil.DeleteAsync($"api/Person/{Id}");
        }
        #endregion Person

        #region Schedule
        public async Task<HttpResponseMessage> GetSchedule()
        {
            return await _clientPerfil.GetAsync("api/Schedule");
        }
        public async Task<HttpResponseMessage> GetScheduleById(int? Id)
        {
            return await _clientPerfil.GetAsync($"api/Schedule/{Id}");
        }
        public async Task<HttpResponseMessage> PostSchedule(Schedule schedule)
        {
            return await _clientPerfil.PostAsJsonAsync("api/Schedule", schedule);
        }
        public async Task<HttpResponseMessage> PutSchedule(Schedule schedule, int? Id)
        {
            return await _clientPerfil.PutAsJsonAsync($"api/Schedule/{Id}", schedule);
        }
        public async Task<HttpResponseMessage> DeleteSchedule(int? Id)
        {
            return await _clientPerfil.DeleteAsync($"api/Schedule/{Id}");
        }
        #endregion Schedule

        #region Pet
        public async Task<HttpResponseMessage> GetPet()
        {
            return await _clientAnimal.GetAsync("api/Pet");
        }
        public async Task<HttpResponseMessage> GetPetById(int? Id)
        {
            return await _clientAnimal.GetAsync($"api/Pet/{Id}");
        }
        public async Task<HttpResponseMessage> PostPet(Pet pet)
        {
            return await _clientAnimal.PostAsJsonAsync("api/Pet", pet);
        }
        public async Task<HttpResponseMessage> PutPet(Pet pet, int? Id)
        {
            return await _clientAnimal.PutAsJsonAsync($"api/Pet/{Id}", pet);
        }
        public async Task<HttpResponseMessage> DeletePet(int? Id)
        {
            return await _clientAnimal.DeleteAsync($"api/Pet/{Id}");
        }
        #endregion Pet
    }
}