using Project.Entities;

namespace MinimalAPI.Models
{
    public class MusicianDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Instruments Instrument { get; set; }

    }
}
