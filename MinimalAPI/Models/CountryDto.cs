using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class CountryDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class CountryCreationDto
    {
        [Required, MaxLength(2), RegularExpression("^[A-Z]{2}$")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class CountryUpdateDto
    {
        public string Name { get; set; }
    }
}
