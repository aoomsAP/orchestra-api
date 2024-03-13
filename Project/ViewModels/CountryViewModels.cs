using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class CountryGetViewModel
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }

    public class CountryCreateViewModel
    {
        [Required, MaxLength(2), RegularExpression("^[A-Z]{2}$")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        //public IEnumerable<Orchestra> Orchestras { get; set; }
    }

    public class CountryUpdateViewModel
    {
        [Required]
        public string Name { get; set; }

        //public IEnumerable<Orchestra> Orchestras { get; set; }
    }
}
