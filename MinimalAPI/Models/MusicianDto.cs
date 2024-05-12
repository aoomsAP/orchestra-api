using Project.Entities;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class MusicianDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Instruments Instrument { get; set; }
    }

    public class MusicianCreationDto
    {
        [Required]
        public string Name { get; set; }

        public Instruments Instrument { get; set; }
    }

    public class MusicianUpdateDto
    {
        [Required]
        public string Name { get; set; }

        public Instruments Instrument { get; set; }
    }

    public class MusicianOrchestrasUpdateDto
    {
        public ICollection<int> OrchestraIds { get; set; }
    }
}
