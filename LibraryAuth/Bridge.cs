using System.Configuration;
using System.Web.Configuration;

namespace LibraryAuth
{
    public class Bridge
    {
        protected string PathConnection = null;

        public string Connect()
        {
            try
            {
                PathConnection = WebConfigurationManager.ConnectionStrings["PetshopAuth"].ConnectionString;
                return PathConnection;
            }
            catch
            {
                PathConnection = ConfigurationManager.ConnectionStrings["PetshopAuth"].ConnectionString;
                return PathConnection;
            }
        }
    }
}
