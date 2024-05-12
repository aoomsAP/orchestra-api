using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Project.Entities
{
    public class Orchestra
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Conductor { get; set; }

        public Country? Country { get; set; }

        public ICollection<Musician> Musicians { get; set; } = new List<Musician>();
    }
}
