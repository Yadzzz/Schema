using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Schema.Pages;
using Serilog;

namespace Schema.Services
{
    public class ScheduleFiltersService
    {
        private Microsoft.Extensions.Logging.ILogger _logger { get; set; }
        private readonly DataAccessLibrary.Context.BevakningContext _bevakningContext;

        public ScheduleFiltersService(DataAccessLibrary.Context.BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;

            var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this._logger = logger;
        }

        public async Task<List<DataAccessLibrary.Models.ScheduleFilter>?> GetFiltersForUserAsync(int userId)
        {
            if (this._bevakningContext == null || this._bevakningContext.ScheduleFilters == null)
                return null;

            try
            {
                return await this._bevakningContext.ScheduleFilters.Where(x => x.UserId == userId).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<bool> AddFilter(DataAccessLibrary.Models.ScheduleFilter filter)
        {
            if (filter == null || this._bevakningContext == null || this._bevakningContext.ScheduleFilters == null) return false;

            try
            {
                await this._bevakningContext.ScheduleFilters.AddAsync(filter);
                await this._bevakningContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> RemoveFilterAsync(DataAccessLibrary.Models.ScheduleFilter filter)
        {
            if (filter == null || this._bevakningContext == null || this._bevakningContext.ScheduleFilters == null) return false;

            try
            {
                this._bevakningContext.ScheduleFilters.Remove(filter);
                await this._bevakningContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return false;
            }
        }
    }
}
