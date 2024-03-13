namespace Project.Entities
{
    public class Orchestra
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Conductor { get; set; }

        public IEnumerable<Musician> Musicians { get; set; }
    }
}
