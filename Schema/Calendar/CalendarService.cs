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
                string firstName = (user.FirstName + " " + user.LastName) ?? string.Empty;
                string monday = "";
                string tuesday = "";
                string wednesday = "";
                string thursday = "";
                string friday = "";
                string saturday = "";
                string sunday = "";

                int mondayId = 0;
                int tuesdayId = 0;
                int wednesdayId = 0;
                int thursdayId = 0;
                int fridayId = 0;
                int saturdayId = 0;
                int sundayId = 0;

                if (user?.FirstName?.ToLower() == "ledigt-pass")
                {
                    firstName = user.FirstName;
                }

                foreach (var date in dates)
                {
                    var scheduledDateForUser = context.Schedules.Where(x => x.UserId == user.Id && x.DateStart.Value.Date == date.Date).FirstOrDefault();

                    if (scheduledDateForUser == null)
                    {
                        continue;
                    }

                    switch (date.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            monday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            mondayId = scheduledDateForUser.Id;
                            break;
                        case DayOfWeek.Tuesday:
                            tuesday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            tuesdayId = scheduledDateForUser.Id;
                            break;
                        case DayOfWeek.Wednesday:
                            wednesday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            wednesdayId = scheduledDateForUser.Id;
                            break;
                        case DayOfWeek.Thursday:
                            thursday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            thursdayId = scheduledDateForUser.Id;
                            break;
                        case DayOfWeek.Friday:
                            friday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            fridayId = scheduledDateForUser.Id;
                            break;
                        case DayOfWeek.Saturday:
                            saturday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            saturdayId = scheduledDateForUser.Id;
                            break;
                        case DayOfWeek.Sunday:
                            sunday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                            sundayId = scheduledDateForUser.Id;
                            break;
                    }
                }

                //foreach (var date in dates)
                //{
                //    var scheduledDateForUser = context.Schedules.Where(x => x.UserId == user.Id && x.DateStart.Value.Date == date.Date).ToList();

                //    if (scheduledDateForUser == null)
                //    {
                //        continue;
                //    }

                //    foreach (var schedule in scheduledDateForUser)
                //    {
                //        switch (date.DayOfWeek)
                //        {
                //            case DayOfWeek.Monday:
                //                monday += schedule?.TimeStart + "-" + schedule?.TimeEnd + " ";
                //                mondayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Tuesday:
                //                tuesday += schedule?.TimeStart + "-" + schedule?.TimeEnd + " ";
                //                tuesdayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Wednesday:
                //                wednesday += schedule?.TimeStart + "-" + schedule?.TimeEnd + " ";
                //                wednesdayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Thursday:
                //                thursday += schedule?.TimeStart + "-" + schedule?.TimeEnd + " ";
                //                thursdayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Friday:
                //                friday += schedule?.TimeStart + "-" + schedule?.TimeEnd + " ";
                //                fridayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Saturday:
                //                saturday += schedule?.TimeStart + "-" + schedule?.TimeEnd + " ";
                //                saturdayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Sunday:
                //                sunday += schedule?.TimeStart + "-" + schedule?.TimeEnd + " ";
                //                sundayId = schedule.Id;
                //                break;
                //        }
                //    }
                //}

                //rows.Add(new CalendarRow(firstName, monday, tuesday, wednesday, thursday, friday, saturday, sunday));
                rows.Add(new CalendarRow(firstName, mondayId, monday, tuesdayId, tuesday, wednesdayId, wednesday, thursdayId, thursday, fridayId, friday, saturdayId, saturday, sundayId, sunday));
            }

            return rows;
        }
    }
}
