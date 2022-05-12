namespace WebAuths.Tests.Controllers
{
    [TestClass]
    public class PetAuthCheckPathTest
    {
        public ApiClient _apiClient;
        public readonly string abosolutePath = @"C:\Users\Luiz Siqueira\Desktop\EDC_Assessment\CSharp\Petshop\Web\Storage\Pet\";


        [TestMethod]
        public void FileNameDoesExists()
        {
            string fileName = "05.jpg";
            FileProcess fileProcess = new FileProcess();
            bool fromCall = fileProcess.FileExists(Path.Combine(abosolutePath, fileName));
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExists()
        {
            FileProcess fileProcess = new FileProcess();
            bool fromCall = fileProcess.FileExists(Path.Combine(abosolutePath, ""));
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
    }
}
