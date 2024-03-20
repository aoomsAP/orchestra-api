using Project.Entities;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class MusicianGetViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Instruments Instrument { get; set; }
    }

    public class MusicianCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Instruments Instrument { get; set; }
    }

    public class MusicianUpdateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Instruments Instrument { get; set; }

        [Required]
        public IEnumerable<int> OrchestraIds { get; set; }

    }
}
