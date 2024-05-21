using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class OrchestraDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Conductor { get; set; }
        public string Country { get; set; }
    }

    public class OrchestraCreationDto
    {
        [Required]
        public string Name { get; set; }

        public string Conductor { get; set; } = null!;

        [RegularExpression("^[A-Z]{2}$")]
        public string CountryCode { get; set; } = null!;
    }

    public class OrchestraUpdateDto
    {
        public string Name { get; set; }

        public string Conductor { get; set; } = null!;

        [RegularExpression("^[A-Z]{2}$")]
        public string CountryCode { get; set; } = null!;
    }

    public class OrchestraMusiciansUpdateDto
    {
        public ICollection<int> MusicianIds { get; set; }
    }
}
