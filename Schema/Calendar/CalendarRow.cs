namespace Schema.Calendar
{
    public class CalendarRow
    {
        public string FirstName { get; set; }

        public int MondayId { get; set; }
        public string Monday { get; set; }

        public int TuesdayId { get; set; }
        public string Tuesday { get; set; }

        public int WednesdayId { get; set; }
        public string Wednesday { get; set; }

        public int ThursdayId { get; set; }
        public string Thursday { get; set; }

        public int FridayId { get; set; }
        public string Friday { get; set; }

        public int SaturdayId { get; set; }
        public string Saturday { get; set; }

        public int SundayId { get; set; }
        public string Sunday { get; set; }

        public CalendarRow(string firstName, string monday, string tuesday, string wednesday, string thursday, string friday, string saturday, string sunday)
        {
            this.FirstName = firstName;
            this.Monday = monday;
            this.Tuesday = tuesday;
            this.Wednesday = wednesday;
            this.Thursday = thursday;
            this.Friday = friday;
            this.Saturday = saturday;
            this.Sunday = sunday;
        }

        public CalendarRow(string firstName, int mondayId, string monday, int tuesdayId, string tuesday, int wednesdayId, string wednesday,
                            int thursdayId, string thursday, int fridayId, string friday, int saturdayId, string saturday, int sundayId, string sunday)
        {
            this.FirstName = firstName;
            this.MondayId = mondayId;
            this.Monday = monday;
            this.TuesdayId = tuesdayId;
            this.Tuesday = tuesday;
            this.WednesdayId = wednesdayId;
            this.Wednesday = wednesday;
            this.ThursdayId = thursdayId;
            this.Thursday = thursday;
            this.FridayId = fridayId;
            this.Friday = friday;
            this.SaturdayId = saturdayId;
            this.Saturday = saturday;
            this.SundayId = sundayId;
            this.Sunday = sunday;
        }
    }
}
