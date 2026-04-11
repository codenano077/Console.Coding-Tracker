namespace coding_Tracker
{
    internal class Coding// this is the model class that will represent the coding session records in the database
    {
        public int Id { get; set; }
        public String Date { get; set; }
        public String Duration { get; set; }
    }
}