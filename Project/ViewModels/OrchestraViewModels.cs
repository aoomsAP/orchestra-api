using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class OrchestraGetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Conductor { get; set; }
    }

    public class OrchestraCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Conductor { get; set; }
    }

    public class OrchestraUpdateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Conductor { get; set; }
    }

    public class OrchestraMusiciansUpdateViewModel
    {
        [Required]
        public IEnumerable<int> MusicianIds { get; set; }
    }
}
