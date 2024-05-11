using System.ComponentModel.DataAnnotations;

namespace Project.Entities
{
    public class Country
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public ICollection<Orchestra> Orchestras { get; set; } = new List<Orchestra>();
    }
}
