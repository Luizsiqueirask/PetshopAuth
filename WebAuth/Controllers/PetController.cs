using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAuth.Api;
using WebAuth.Models.Animal;
using WebAuth.Models.Perfil;

namespace WebAuth.Controllers
{
    public class PetController : Controller
    {
        private readonly ApiClient _clientPet;
        private readonly BlobClient _blobClient;
        internal readonly string directoryPath = @"../Storage/Pet/";

        public PetController()
        {
            _clientPet = new ApiClient();
            _blobClient = new BlobClient();
        }

        // GET: Pet
        public async Task<ActionResult> Index()
        {
            var allPets = await _clientPet.GetPet();
            var allPeople = await _clientPet.GetPerson();
            var containerPersonPet = new List<PeoplePets>();

            if (allPets.IsSuccessStatusCode)
            {
                var pets = await allPets.Content.ReadAsAsync<IEnumerable<Pet>>();
                var people = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();

                if (allPeople.IsSuccessStatusCode)
                {

                    foreach (var pet in pets)
                    {
                        foreach (var person in people)
                        {
                            if (pet.PersonId.Equals(person.Id))
                            {
                                containerPersonPet.Add(new PeoplePets()
                                {
                                    Pets = pet,
                                    People = person,
                                    PeopleSelect = new List<SelectListItem>() {
                                        new SelectListItem()
                                        {
                                            Value = pet.Id.ToString(),
                                            Text = pet.Name,
                                            Selected = pet.PersonId == person.Id
                                        }
                                    }
                                });
                            }
                        }
                    }
                    return View(containerPersonPet);
                }
            }
            return View(new List<Person>());
        }
        // GET: Pet/Details/5
        public async Task<ActionResult> Details(int? Id)
        {
            var allPets = await _clientPet.GetPetById(Id);
            var personPet = new PersonPet();

            if (allPets.IsSuccessStatusCode)
            {
                var pet = await allPets.Content.ReadAsAsync<Pet>();
                var allPeople = await _clientPet.GetPersonById(pet.PersonId);

                if (allPeople.IsSuccessStatusCode)
                {
                    var person = await allPeople.Content.ReadAsAsync<Person>();

                    if (pet.PersonId.Equals(person.Id))
                    {
                        personPet = new PersonPet()
                        {
                            Pet = pet,
                            Person = person,
                            PersonPetsSelect = new SelectListItem()
                            {
                                Value = pet.Id.ToString(),
                                Text = pet.Name,
                                Selected = pet.PersonId == person.Id
                            }
                        };
                    }
                    return View(personPet);
                }
            }
            return View(new PersonPet());
        }

        // GET: Pet/Create
        public async Task<ActionResult> Create()
        {
            var allPerson = await _clientPet.GetPerson();

            if (allPerson.IsSuccessStatusCode)
            {
                var people = await allPerson.Content.ReadAsAsync<IEnumerable<Person>>();
                var selectPetsList = new List<SelectListItem>();
                var pet = new Pet();

                foreach (var person in people)
                {
                    var selectPet = new SelectListItem()
                    {
                        Value = person.Id.ToString(),
                        Text = $"Nome: {person.FirstName} {person.LastName}",
                        Selected = person.Id == pet.PersonId
                    };
                    selectPetsList.Add(selectPet);
                    pet.PeopleSelect = selectPetsList;
                    //pet.PersonId = (int)Convert.ToUInt32(selectPet.Value.ToString());
                }
                return View(pet);
            }
            return View(new Pet());
        }

        // POST: Pet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Pet pet)
        {
            HttpFileCollectionBase httpFileCollection = Request.Files;
            HttpPostedFileBase postedFileBase = httpFileCollection[0];

            try
            {
                if (ModelState.IsValid)
                {
                    await _blobClient.SetupCloudBlob();

                    var imageNameBlob = _blobClient.GetRandomBlobName(httpFileCollection[0].FileName);
                    var imagePathblob = _blobClient._blobContainer.GetBlockBlobReference(imageNameBlob);
                    await imagePathblob.UploadFromStreamAsync(httpFileCollection[0].InputStream);

                    pet.Image.Tag = imagePathblob.Name.ToString();
                    pet.Image.Path = imagePathblob.Uri.AbsolutePath.ToString();
                    await _clientPet.PostPet(pet);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                if (ModelState.IsValid)
                {
                    // Create pictute on server
                    var imageName = Path.GetFileName(httpFileCollection[0].FileName);
                    var rootPath = Server.MapPath(directoryPath);
                    var picturePath = Path.Combine(rootPath, imageName);

                    // Add picture reference to model and save
                    var PictureExt = Path.GetExtension(imageName);

                    if (PictureExt.Equals(".jpg") || PictureExt.Equals(".jpeg") || PictureExt.Equals(".png"))
                    {
                        pet.Image.Tag = imageName;
                        pet.Image.Path = picturePath;
                        postedFileBase.SaveAs(picturePath);
                        await _clientPet.PostPet(pet);

                        return RedirectToAction("Index");
                    }
                }
            }
            return View(new Pet());
        }

        // GET: Pet/Edit/5
        public async Task<ActionResult> Edit(int? Id)
        {
            var allPets = await _clientPet.GetPetById(Id);

            if (allPets.IsSuccessStatusCode)
            {
                var pet = await allPets.Content.ReadAsAsync<Pet>();
                var allPeople = await _clientPet.GetPersonById(pet.PersonId);

                if (allPeople.IsSuccessStatusCode)
                {
                    var person = await allPeople.Content.ReadAsAsync<Person>();

                    var personPet = new PersonPet()
                    {
                        Pet = pet,
                        Person = person,
                        PersonPetsSelect = new SelectListItem()
                        {
                            Value = pet.Id.ToString(),
                            Text = pet.Name,
                            Selected = pet.PersonId == person.Id
                        }
                    };

                    return View(personPet);
                }
            }
            return View(new PersonPet());
        }

        // POST: Pet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Pet pet, int? Id)
        {
            HttpFileCollectionBase httpFileCollection = Request.Files;
            HttpPostedFileBase postedFileBase = httpFileCollection[0];

            try
            {
                if (ModelState.IsValid)
                {
                    await _blobClient.SetupCloudBlob();

                    var imageNameBlob = _blobClient.GetRandomBlobName(httpFileCollection[0].FileName);
                    var imagePathblob = _blobClient._blobContainer.GetBlockBlobReference(imageNameBlob);
                    await imagePathblob.UploadFromStreamAsync(httpFileCollection[0].InputStream);

                    pet.Image.Tag = imagePathblob.Name.ToString();
                    pet.Image.Path = imagePathblob.Uri.AbsolutePath.ToString();

                    await _clientPet.PostPet(pet);
                }
            }
            catch
            {
                if (ModelState.IsValid)
                {
                    // Create pictute on server
                    var imageName = Path.GetFileName(httpFileCollection[0].FileName);
                    var rootPath = Server.MapPath(directoryPath);
                    var picturePath = Path.Combine(rootPath, imageName);

                    // Add picture reference to model and save
                    var PictureExt = Path.GetExtension(imageName);

                    if (PictureExt.Equals(".jpg") || PictureExt.Equals(".jpeg") || PictureExt.Equals(".png"))
                    {
                        pet.Image.Tag = imageName;
                        pet.Image.Path = picturePath;

                        postedFileBase.SaveAs(picturePath);
                        await _clientPet.PutPet(pet, Id);

                        return RedirectToAction("Index");
                    }
                }
            }
            return View(new Person());
        }

        // GET: Pet/Delete/5
        public async Task<ActionResult> Delete(int? Id)
        {
            var pets = await _clientPet.GetPetById(Id);

            if (pets.IsSuccessStatusCode)
            {
                var pet = await pets.Content.ReadAsAsync<Pet>();
                return View(pet);
            }

            return View(new Pet());
        }

        // POST: Pet/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                // TODO: Add delete logic here
                var pet = await _clientPet.DeletePet(Id);

                if (pet.IsSuccessStatusCode)
                {
                    await pet.Content.ReadAsAsync<Pet>();
                    return View(pet);
                }
            }
            catch (Exception ex)
            {
                return View($"MSG: {ex.Message}");
            }
            return View(new Pet());
        }
    }
}