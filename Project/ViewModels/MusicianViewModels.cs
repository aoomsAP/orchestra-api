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
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required, EnumDataType(typeof(Instruments))]
        public Instruments Instrument { get; set; }
    }

    public class MusicianUpdateViewModel
    {
        public string Name { get; set; }

        [EnumDataType(typeof(Instruments))]
        public Instruments Instrument { get; set; }
    }

    public class MusicianOrchestrasUpdateViewModel
    {
        [Required]
        public IEnumerable<int> OrchestraIds { get; set; }
    }
}
