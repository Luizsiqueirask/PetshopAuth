namespace Web.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTest
    {
        public ApiClient _apiClient;
        public readonly string rootPath = @"C:\Users\Luiz Siqueira\Desktop\EDC_Assessment\CSharp\Petshop\Web\Storage\Person\";

        public PersonControllerTest()
        {
            _apiClient = new ApiClient();
        }

        [TestMethod]
        public async void FileNameUploadsExists()
        {
            int Id = 1;
            var people = await _apiClient.GetPersonById(Id);

            if (people.IsSuccessStatusCode)
            {
                var person = await people.Content.ReadAsAsync<Person>();
                FileProcess fileProcess = new FileProcess();
                bool fromCall = fileProcess.FileExists(person.Picture.Path);
                Assert.IsTrue(fromCall);
            }
        }

        [TestMethod]
        public void FileNameDoesExists()
        {
            string fileName = "01.jpg";
            FileProcess fileProcess = new FileProcess();
            bool fromCall = fileProcess.FileExists(Path.Combine(rootPath, fileName));
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExists()
        {
            bool fromCall;
            string fileName = @"C:\Regedit.exe";
            FileProcess fileProcess = new FileProcess();
            fromCall = fileProcess.FileExists(fileName);
            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmptyThrows()
        {
            FileProcess fileProcess = new FileProcess();
            fileProcess.FileExists("");
        }

        [TestMethod]
        public void FileNameNullOrEmptyTryCatch()
        {
            FileProcess fileProcess = new FileProcess();

            try
            {
                fileProcess.FileExists("");
            }
            catch (ArgumentException)
            {
                return;
            }
            Assert.Fail("Fail Expected");
        }

        [TestMethod]
        public void Get()
        {
            // https://www.thecodebuzz.com/unit-testing-controller-sync-and-async-methods-asp-net-core-example/
            var person = new Person()
            {
                Id = 1,
                FirstName = "Luiz",
                LastName = "Siqueira",
                Age = 32,
                Birthday = DateTime.Now,
                Genre = "Male",
                Picture = new Picture() { Id = 1, Tag = "", Path = "" },
                Contact = new Contact() { Id = 1, Email = "", Mobile = "" },
                Address = new Address() { Id = 1, Country = "", States = "", City = "", Neighborhoods = "" }
            };

            // Arrange
            PersonController personController = new PersonController();

            // Act
            var personResult = personController.Index();

            // Assert
            Assert.IsNotNull(personResult);
            Assert.AreEqual(person.Id, ((Person)personResult).Id);
            Assert.AreEqual(person.FirstName, ((Person)personResult).FirstName);
            //Assert.AreEqual("Siqueira", personnResult.ElementAt(1));
        }
    }
}
