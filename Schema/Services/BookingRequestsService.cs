using Microsoft.EntityFrameworkCore;

namespace Schema.Services
{
    public class BookingRequestsService
    {
        private DataAccessLibrary.Context.BevakningContext _bevakningContext;

        public BookingRequestsService(DataAccessLibrary.Context.BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;
        }

        public async Task<List<DataAccessLibrary.Models.ScheduleRequests>?> GetBookingRequests()
        {
            if (this._bevakningContext == null || this._bevakningContext.ScheduleRequests == null)
            {
                return null;
            }

            return await this._bevakningContext.ScheduleRequests.Where(x => x.Active).ToListAsync();
        }

        public bool DoesRequestExistsForBooking(int userId, int bookingId)
        {
            if (this._bevakningContext == null || this._bevakningContext.ScheduleRequests == null)
            {
                return false;
            }

            try
            {
                var request = this._bevakningContext.ScheduleRequests.Where(x => x.UserId == userId && x.ScheduleId == bookingId).FirstOrDefault();

                return request != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddBookingRequest(int userId, int bookingId)
        {
            if (this._bevakningContext == null || this._bevakningContext.ScheduleRequests == null)
            {
                return false;
            }

            try
            {
                await this._bevakningContext.ScheduleRequests.AddAsync(new DataAccessLibrary.Models.ScheduleRequests()
                {
                    UserId = userId,
                    ScheduleId = bookingId,
                    Accepted = false,
                    Active = true,
                });

                await this._bevakningContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBookingRequest(DataAccessLibrary.Models.ScheduleRequests request)
        {
            if (this._bevakningContext == null || this._bevakningContext.ScheduleRequests == null)
            {
                return false;
            }

            try
            {
                //this._bevakningContext.Entry(request).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this._bevakningContext.ScheduleRequests.Update(request);
                await this._bevakningContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveBookingRequest(DataAccessLibrary.Models.ScheduleRequests request)
        {
            if (this._bevakningContext == null || this._bevakningContext.ScheduleRequests == null)
            {
                return false;
            }

            try
            {
                this._bevakningContext.Remove(request);
                await this._bevakningContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
