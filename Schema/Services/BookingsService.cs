using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Schema.Services
{
    public class BookingsService
    {
        private Microsoft.Extensions.Logging.ILogger _logger { get; set; }
        private readonly DataAccessLibrary.Context.BevakningContext _bevakningContext;

        public BookingsService(DataAccessLibrary.Context.BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;

            var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this._logger = logger;
        }

        public async Task<List<DataAccessLibrary.Models.Schedule>?> GetBookings()
        {
            if (this._bevakningContext == null || this._bevakningContext.Schedules == null)
            {
                return null;
            }

            try
            {
                return await this._bevakningContext.Schedules.ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<DataAccessLibrary.Models.Schedule?> GetBooking(int bookingId)
        {
            if (this._bevakningContext == null || this._bevakningContext.Schedules == null)
            {
                return null;
            }

            try
            {
                return await this._bevakningContext.Schedules.FindAsync(bookingId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<List<DataAccessLibrary.Models.Schedule>?> GetBookingsForUser(int userId)
        {
            if (this._bevakningContext == null || this._bevakningContext.Schedules == null)
            {
                return null;
            }

            try
            {
                return await this._bevakningContext.Schedules.Where(x => x.UserId == userId).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<bool> AddBooking(DataAccessLibrary.Models.Schedule schedule)
        {
            if (this._bevakningContext == null || this._bevakningContext.Schedules == null)
            {
                return false;
            }

            try
            {
                await this._bevakningContext.Schedules.AddAsync(schedule);
                await this._bevakningContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateBooking(DataAccessLibrary.Models.Schedule schedule)
        {
            if (this._bevakningContext == null || this._bevakningContext.Schedules == null)
            {
                return false;
            }

            try
            {
                this._bevakningContext.Schedules.Update(schedule);
                await this._bevakningContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteBooking(DataAccessLibrary.Models.Schedule schedule)
        {
            if (this._bevakningContext == null || this._bevakningContext.Schedules == null)
            {
                return false;
            }

            try
            {
                this._bevakningContext.Schedules.Remove(schedule);
                await this._bevakningContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return false;
            }

            return true;
        }
    }
}
