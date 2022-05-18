using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAuth.Api;
using WebAuth.Models.Perfil;

namespace WebAuth.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        protected readonly ApiClient _clientPerson;
        protected readonly BlobClient _blobClient;
        protected readonly string directoryPath = @"../Storage/Person/";

        public PersonController()
        {
            _clientPerson = new ApiClient();
            _blobClient = new BlobClient();
        }

        public PersonController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Person
        public async Task<ActionResult> Index()
        {
            var allPeople = await _clientPerson.GetPerson();
            if (allPeople.IsSuccessStatusCode)
            {
                var people = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();
                return View(people);
            }

            return View(new List<Person>());
        }

        // GET: Person/Details/5
        public async Task<ActionResult> Details(int? Id)
        {
            var people = await _clientPerson.GetPersonById(Id);

            if (people.IsSuccessStatusCode)
            {
                var person = await people.Content.ReadAsAsync<Person>();
                return View(person);
            }
            return View(new Person());
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View(new Person());
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person person)
        {
            // https://www.c-sharpcorner.com/article/upload-and-display-image-in-asp-net-core-3-1/
            // https://docs.microsoft.com/pt-br/dotnet/api/system.web.ui.webcontrols.fileupload.postedfile?view=netframework-4.8
            // https://cpratt.co/file-uploads-in-asp-net-mvc-with-view-models/

            HttpFileCollectionBase httpFileCollection = Request.Files;
            HttpPostedFileBase postedFileBase = httpFileCollection[0];

            try
            {
                if (ModelState.IsValid)
                {
                    await _blobClient.SetupCloudBlob();

                    var pictureNameBlob = _blobClient.GetRandomBlobName(httpFileCollection[0].FileName);
                    var picturePathblob = _blobClient._blobContainer.GetBlockBlobReference(pictureNameBlob);
                    await picturePathblob.UploadFromStreamAsync(httpFileCollection[0].InputStream);

                    person.Picture.Tag = picturePathblob.Name.ToString();
                    person.Picture.Path = picturePathblob.Uri.AbsolutePath.ToString();
                    await _clientPerson.PostPerson(person);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                if (ModelState.IsValid)
                {
                    // Create pictute on server
                    var pictureName = Path.GetFileName(httpFileCollection[0].FileName);
                    var rootPath = Server.MapPath(directoryPath);
                    var picturePath = Path.Combine(rootPath, pictureName);
                    var pathReal = directoryPath + pictureName;

                    // Add picture reference to model and save
                    var PictureExt = Path.GetExtension(pictureName);


                    if (PictureExt.Equals(".jpg") || PictureExt.Equals(".jpeg") || PictureExt.Equals(".png"))
                    {
                        person.Picture.Tag = pictureName;
                        person.Picture.Path = pathReal;
                        postedFileBase.SaveAs(picturePath);
                        await _clientPerson.PostPerson(person);

                        return RedirectToAction("Index");
                    }
                }
            }
            return View(new Person());
        }

        // GET: Person/Edit/5
        public async Task<ActionResult> Edit(int? Id)
        {
            var people = await _clientPerson.GetPersonById(Id);

            if (people.IsSuccessStatusCode)
            {
                var person = await people.Content.ReadAsAsync<Person>();
                return View(person);
            }
            return View(new Person());
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Person person, int Id)
        {
            HttpFileCollectionBase httpFileCollection = Request.Files;
            HttpPostedFileBase postedFileBase = httpFileCollection[0];

            try
            {
                if (ModelState.IsValid)
                {
                    await _blobClient.SetupCloudBlob();

                    var pictureNameBlob = _blobClient.GetRandomBlobName(httpFileCollection[0].FileName);
                    var picturePathblob = _blobClient._blobContainer.GetBlockBlobReference(pictureNameBlob);
                    await picturePathblob.UploadFromStreamAsync(httpFileCollection[0].InputStream);

                    person.Picture.Tag = picturePathblob.Name.ToString();
                    person.Picture.Path = picturePathblob.Uri.AbsolutePath.ToString();
                    await _clientPerson.PostPerson(person);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                if (ModelState.IsValid)
                {
                    // Create pictute on server
                    var pictureName = Path.GetFileName(httpFileCollection[0].FileName);
                    var rootPath = Server.MapPath(directoryPath);
                    var picturePath = Path.Combine(rootPath, pictureName);
                    var pathReal = directoryPath + pictureName;

                    // Add picture reference to model and save
                    var PictureExt = Path.GetExtension(pictureName);


                    if (PictureExt.Equals(".jpg") || PictureExt.Equals(".jpeg") || PictureExt.Equals(".png"))
                    {
                        person.Picture.Tag = pictureName;
                        person.Picture.Path = pathReal;
                        postedFileBase.SaveAs(picturePath);
                        await _clientPerson.PostPerson(person);

                        return RedirectToAction("Index");
                    }
                }
            }
            return View(new Person());
        }

        // GET: Person/Delete/5
        public async Task<ActionResult> Delete(int? Id)
        {
            try
            {
                var people = await _clientPerson.GetPersonById(Id);

                if (people.IsSuccessStatusCode)
                {
                    var person = await people.Content.ReadAsAsync<Person>();
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG: {ex.Message}");
            }

            return View(new Person());
        }

        // POST: Person/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                var person = await _clientPerson.DeletePerson(Id);

                if (person.IsSuccessStatusCode)
                {
                    await person.Content.ReadAsAsync<Person>();
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG: {ex.Message}");
            }
            return View(new Person());
        }
    }
}
