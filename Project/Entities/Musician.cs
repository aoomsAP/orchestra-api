namespace Project.Entities
{
    public enum Instruments
    {
        Violin, Viola, Cello, DoubleBass, // strings
        Flute, Oboe, Clarinet, Bassoon, Saxophone, // woodwinds
        Horn, Trumpet, Trombone, Tuba, // brass
        Percussion, // specific instrument (timpani, glockenspiel, cymbals, etc) often unlisted therefore one broad category
        Piano, Harpsichord, Harp, Guitar, Mandolin, Lute // plucked instruments
    }

    public class Musician
    {
        // Entity Framework returns empty list as null
        // therefore it is advised to initialize the list in the constructor of the entity
        // https://stackoverflow.com/questions/9246729/why-does-entity-framework-return-null-list-instead-of-empty-ones
        //public Musician()
        //{
        //    Orchestras = new List<Orchestra>();
        //}

        public int Id { get; set; }

        public string Name { get; set; }

        public Instruments Instrument { get; set; }

        public List<Orchestra> Orchestras { get; set; }
    }
}
