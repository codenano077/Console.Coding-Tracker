namespace coding_Tracker
{
    internal class StopWatch
    {
        private DateTime startTime;
        private DateTime endTime;
        private TimeSpan duration;

        internal void Start()
        {
            startTime = DateTime.Now;
            Console.WriteLine("\n StopWatch started at: " + startTime);
        }

        internal void Stop()
        {
            endTime = DateTime.Now;
            duration = endTime - startTime;
            Console.WriteLine("\n StopWatch stopped at: " + endTime);
            Console.WriteLine("\n Duration: " + duration);

            Coding coding = new();
            coding.Date = startTime.ToString("yyyy-MM-dd");
            coding.Duration = duration.ToString(@"hh\:mm");

            CodingController codingController = new();
            codingController.Post(coding);
            Console.WriteLine("\n Coding session recorded in the database.");   
        }
    }
}