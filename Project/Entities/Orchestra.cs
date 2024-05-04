namespace Project.Entities
{
    public class Orchestra
    {
        // Entity Framework returns empty list as null
        // therefore it is advised to initialize the list in the constructor of the entity
        // https://stackoverflow.com/questions/9246729/why-does-entity-framework-return-null-list-instead-of-empty-ones
        //public Orchestra()
        //{
        //    Musicians = new List<Musician>();
        //}

        public int Id { get; set; }

        public string Name { get; set; }

        public string Conductor { get; set; }

        public List<Musician> Musicians { get; set; }
    }
}
