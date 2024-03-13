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

        // IEnumerable of Musicians?
    }

    public class OrchestraUpdateViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Conductor { get; set; }

        // IEnumerable of Musicians?

    }
}
