namespace Project.Entities
{
    public class Country
    {
        // Entity Framework returns empty list as null
        // this results in a NullReferenceException when attempting to loop through the list
        // an obvious choice would be to do a null-check before looping through the list
        // 
        // therefore it is advised to initialize the list in the constructor of the entity
        // https://stackoverflow.com/questions/9246729/why-does-entity-framework-return-null-list-instead-of-empty-ones
        //public Country()
        //{
        //    Orchestras = new List<Orchestra>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public List<Orchestra> Orchestras { get; set; }
    }
}
