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
        public int Id { get; set; }

        public string Name { get; set; }

        public Instruments Instrument { get; set; }

        public IEnumerable<Orchestra> Orchestras { get; set; }
    }
}
