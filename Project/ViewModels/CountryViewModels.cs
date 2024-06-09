using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class CountryGetViewModel
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }

    public class CountryCreateViewModel
    {
        [Required, RegularExpression("^[A-Z]{2}$")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }

    public class CountryUpdateViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }

    public class CountryOrchestrasUpdateViewModel
    {
        [Required]
        public IEnumerable<int> OrchestraIds { get; set; }
    }
}
