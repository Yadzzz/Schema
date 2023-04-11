namespace Schema.Calendar
{
    public class Calendar
    {
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }

        public List<CalendarRow> Rows { get; set; }

        public Calendar(DateTime weekStarDate, DateTime weekEndDate, List<CalendarRow> rows)
        {
            this.WeekStartDate= weekStarDate;
            this.WeekEndDate= weekEndDate;
            this.Rows = rows;
        }
    }
}
