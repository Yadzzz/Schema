namespace Schema.Calendar
{
    public class CalendarService
    {
        public Calendar InitializeCalendarData(DateTime date)
        {
            DateManipulator dateManipulator = new(date);
            DateTime[] dates = dateManipulator.GetDatesOfweek();

            List<CalendarRow> rows = this.GetCalendarRows(dates);

            return new Calendar(dates[0], dates[7], rows);
        }

        private List<CalendarRow> GetCalendarRows(DateTime[] dates)
        {
            List<CalendarRow> rows = new List<CalendarRow>();

            //Get all users
            //Foreach all users with all dates of the week
            //Create a row
        }
    }
}
