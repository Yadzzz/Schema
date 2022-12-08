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
            DateTime[] dates = this.ExtractAllDatesOfWeek();

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
    }
}
