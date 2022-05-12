using System.ComponentModel.DataAnnotations;

namespace WebAuth.Models.Animal
{
    public class Image
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string Tag { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Path { get; set; }
    }
}
