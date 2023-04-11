namespace Schema.Calendar
{
    public class CalendarService
    {
        public Calendar InitializeCalendarData(DateTime date)
        {
            DateManipulator dateManipulator = new(date);
            DateTime[] dates = dateManipulator.GetDatesOfweek();

            List<CalendarRow> rows = this.GetCalendarRows(dates);

            return new Calendar(dates[0], dates[6], rows);
        }

        private List<CalendarRow> GetCalendarRows(DateTime[] dates)
        {
            List<CalendarRow> rows = new List<CalendarRow>();

            var context = new DataAccessLibrary.Context.BevakningContext();

            if (context == null || context.Users == null || context.Schedules == null)
            {
                return rows;
            }

            //Get all users
            var users = context.Users.ToList();

            if (users == null || users.Count == 0)
            {
                return rows;
            }

            //Foreach all users with all dates of the week
            //Create a row
            foreach (var user in users)
            {
                string firstName = user.FirstName ?? string.Empty;
                string monday = "";
                string tuesday = "";
                string wednesday = "";
                string thursday = "";
                string friday = "";
                string saturday = "";
                string sunday = "";

                foreach(var date in dates)
                {
                    var scheduledDateForUser = context.Schedules.Where(x => x.UserId == user.Id && x.DateStart.Value.Date == date).FirstOrDefault();

                    switch(date.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            monday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            break;
                        case DayOfWeek.Tuesday:
                            tuesday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            break;
                        case DayOfWeek.Wednesday:
                            wednesday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            break;
                        case DayOfWeek.Thursday:
                            thursday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            break;
                        case DayOfWeek.Friday:
                            friday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            break;
                        case DayOfWeek.Saturday:
                            saturday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            break;
                        case DayOfWeek.Sunday:
                            sunday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            break;
                    }
                }

                rows.Add(new CalendarRow(firstName, monday, tuesday, wednesday, thursday, friday, saturday, sunday));
            }

            return rows;
        }
    }
}
