using System.ComponentModel.DataAnnotations;

namespace WebAuth.Models.Perfil
{
    public class Picture
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]

        public string Tag { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Path { get; set; }
    }
}
