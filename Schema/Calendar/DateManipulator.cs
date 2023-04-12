using Microsoft.AspNetCore.Components.Forms;

namespace Schema.Calendar
{
    public class DateManipulator
    {
        private DateTime _date { get; set; }

        public DateManipulator(DateTime date)
        {
            this._date = date;
        }

        public DateTime[] GetDatesOfweek()
        {
            DateTime[] dates = this.ExtractAllDatesOfWeekV2();

            return dates;
        }

        private DateTime[] ExtractAllDatesOfWeek()
        {
            List<DateTime> dates = new List<DateTime>();

            DateTime date = this._date;
            int day = (int)DateTime.Now.DayOfWeek;
            DateTime monday = date.Date.AddDays((-1) * (day == 0 ? 6 : day - 1));

            for(int i = 0; i < 7; i++)
            {
                dates.Add(monday.AddDays(i));
            }

            return dates.ToArray();
        }

        private DateTime[] ExtractAllDatesOfWeekV2()
        {
            int daysUntilMonday = ((int)this._date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime startOfWeek = this._date.AddDays(-daysUntilMonday);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            // Create array to hold the days of the same week
            DateTime[] daysOfWeek = new DateTime[7];

            // Populate array with the days of the same week
            for (int i = 0; i < 7; i++)
            {
                daysOfWeek[i] = startOfWeek.AddDays(i);
            }

            return daysOfWeek.ToArray();
        }
    }
}
