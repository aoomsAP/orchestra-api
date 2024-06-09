using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class OrchestraGetViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Conductor { get; set; }

        public string? Country { get; set; }

    }

    public class OrchestraCreateViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Conductor { get; set; }

        [RegularExpression("^[A-Z]{2}$")]
        public string CountryCode { get; set; }
    }

    public class OrchestraUpdateViewModel
    {
        public string Name { get; set; }

        public string Conductor { get; set; }

        [RegularExpression("^[A-Z]{2}$")]
        public string CountryCode { get; set; }
    }

    public class OrchestraMusiciansUpdateViewModel
    {
        [Required]
        public IEnumerable<int> MusicianIds { get; set; }
    }
}
