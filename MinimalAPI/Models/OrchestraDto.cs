using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class OrchestraDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Conductor { get; set; }
    }

    public class OrchestraCreationDto
    {
        [Required]
        public string Name { get; set; }
        public string Conductor { get; set; }
    }

    public class OrchestraUpdateDto
    {
        public string Name { get; set; }
        public string Conductor { get; set; }
    }
}
