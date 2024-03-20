namespace Project.Entities
{
    public class Country
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public List<Orchestra> Orchestras { get; set; }
    }
}
