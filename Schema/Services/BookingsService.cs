using Microsoft.EntityFrameworkCore;

namespace Schema.Services
{
    public class BookingsService
    {
        private readonly DataAccessLibrary.Context.BevakningContext _bevakningContext;

        public BookingsService(DataAccessLibrary.Context.BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;
        }

        public async Task<List<DataAccessLibrary.Models.Schedule>?> GetBookings()
        {
            if(this._bevakningContext == null || this._bevakningContext.Schedules == null)
            {
                return null;
            }

            return await this._bevakningContext.Schedules.ToListAsync();
        }
    }
}
