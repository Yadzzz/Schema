namespace Schema.Calendar
{
    public class CalendarRow
    {
        public string FirstName { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }

        public CalendarRow(string firstName, string monday, string tuesday, string wednesday, string thursday, string friday, string saturday, string sunday)
        {
            this.FirstName= firstName;
            this.Monday= monday;
            this.Tuesday= tuesday;
            this.Wednesday= wednesday;
            this.Thursday= thursday;
            this.Friday= friday;
            this.Saturday= saturday;
            this.Sunday= sunday;
        }
    }
}
