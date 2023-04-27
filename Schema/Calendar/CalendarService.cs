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

                List<int> mondayIds = new();
                List<int> tuesdayIds = new();
                List<int> wednesdayIds = new();
                List<int> thursdayIds = new();
                List<int> fridayIds = new();
                List<int> saturdayIds = new();
                List<int> sundayIds = new();

                if (user?.FirstName?.ToLower() == "ledigt-pass")
                {
                    firstName = user.FirstName;
                }

                //foreach (var date in dates)
                //{
                //    var scheduledDateForUser = context.Schedules.Where(x => x.UserId == user.Id && x.DateStart.Value.Date == date.Date).FirstOrDefault();

                //    if (scheduledDateForUser == null)
                //    {
                //        continue;
                //    }

                //    switch (date.DayOfWeek)
                //    {
                //        case DayOfWeek.Monday:
                //            monday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                //            mondayId = scheduledDateForUser.Id;
                //            break;
                //        case DayOfWeek.Tuesday:
                //            tuesday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                //            tuesdayId = scheduledDateForUser.Id;
                //            break;
                //        case DayOfWeek.Wednesday:
                //            wednesday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                //            wednesdayId = scheduledDateForUser.Id;
                //            break;
                //        case DayOfWeek.Thursday:
                //            thursday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                //            thursdayId = scheduledDateForUser.Id;
                //            break;
                //        case DayOfWeek.Friday:
                //            friday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                //            fridayId = scheduledDateForUser.Id;
                //            break;
                //        case DayOfWeek.Saturday:
                //            saturday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                //            saturdayId = scheduledDateForUser.Id;
                //            break;
                //        case DayOfWeek.Sunday:
                //            sunday = scheduledDateForUser?.TimeStart + "-" + scheduledDateForUser?.TimeEnd;
                //            sundayId = scheduledDateForUser.Id;
                //            break;
                //    }
                //}

                //foreach (var date in dates)
                //{
                //    var scheduledDateForUser = context.Schedules.Where(x => x.UserId == user.Id && x.DateStart.Value.Date == date.Date).ToList();

                //    if (scheduledDateForUser == null)
                //    {
                //        continue;
                //    }

                //    int irriation = 0;
                //    foreach (var schedule in scheduledDateForUser)
                //    {
                //        switch (date.DayOfWeek)
                //        {
                //            case DayOfWeek.Monday:
                //                monday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                //                if(scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                //                {
                //                    monday += "<br /><br />";
                //                }
                //                mondayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Tuesday:
                //                tuesday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                //                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                //                {
                //                    tuesday += "<br /><br />";
                //                }
                //                tuesdayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Wednesday:
                //                wednesday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                //                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                //                {
                //                    wednesday += "<br /><br />";
                //                }
                //                wednesdayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Thursday:
                //                thursday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                //                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                //                {
                //                    thursday += "<br /><br />";
                //                }
                //                thursdayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Friday:
                //                friday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                //                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                //                {
                //                    friday += "<br /><br />";
                //                }
                //                fridayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Saturday:
                //                saturday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                //                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                //                {
                //                    saturday += "<br /><br />";
                //                }
                //                saturdayId = schedule.Id;
                //                break;
                //            case DayOfWeek.Sunday:
                //                sunday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                //                if (scheduledDateForUser.Count > 1 && irriation % 2 == 01)
                //                {
                //                    sunday += "<br /><br />";
                //                }
                //                sundayId = schedule.Id;
                //                break;
                //        }
                //    }
                //}

                foreach (var date in dates)
                {
                    var scheduledDateForUser = context.Schedules.Where(x => x.UserId == user.Id && x.DateStart.Value.Date == date.Date).ToList();

                    if (scheduledDateForUser == null)
                    {
                        continue;
                    }

                    int irriation = 0;
                    foreach (var schedule in scheduledDateForUser)
                    {
                        switch (date.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                monday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                                {
                                    monday += "<br /><br />";
                                }
                                mondayIds.Add(schedule.Id);
                                break;
                            case DayOfWeek.Tuesday:
                                tuesday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                                {
                                    tuesday += "<br /><br />";
                                }
                                tuesdayIds.Add(schedule.Id);
                                break;
                            case DayOfWeek.Wednesday:
                                wednesday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                                {
                                    wednesday += "<br /><br />";
                                }
                                wednesdayIds.Add(schedule.Id);
                                break;
                            case DayOfWeek.Thursday:
                                thursday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                                {
                                    thursday += "<br /><br />";
                                }
                                thursdayIds.Add(schedule.Id);
                                break;
                            case DayOfWeek.Friday:
                                friday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                                {
                                    friday += "<br /><br />";
                                }
                                fridayIds.Add(schedule.Id);
                                break;
                            case DayOfWeek.Saturday:
                                saturday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                                if (scheduledDateForUser.Count > 1 && irriation % 2 == 0)
                                {
                                    saturday += "<br /><br />";
                                }
                                saturdayIds.Add(schedule.Id);
                                break;
                            case DayOfWeek.Sunday:
                                sunday += schedule?.TimeStart + "-" + schedule?.TimeEnd;
                                if (scheduledDateForUser.Count > 1 && irriation % 2 == 01)
                                {
                                    sunday += "<br /><br />";
                                }
                                sundayIds.Add(schedule.Id);
                                break;
                        }
                    }
                }

                //rows.Add(new CalendarRow(firstName, monday, tuesday, wednesday, thursday, friday, saturday, sunday));
                //rows.Add(new CalendarRow(firstName, mondayId, monday, tuesdayId, tuesday, wednesdayId, wednesday, thursdayId, thursday, fridayId, friday, saturdayId, saturday, sundayId, sunday));
                rows.Add(new CalendarRow(firstName, mondayIds, monday, tuesdayIds, tuesday, wednesdayIds, wednesday, thursdayIds, thursday, fridayIds, friday, saturdayIds, saturday, sundayIds, sunday));
            }

            return rows;
        }
    }
}
