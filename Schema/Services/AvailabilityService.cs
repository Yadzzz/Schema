using DataAccessLibrary.Context;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Schema.Services
{
    public class AvailabilityService
    {
        private Microsoft.Extensions.Logging.ILogger _logger { get; set; }
        private BevakningContext _bevakningContext;

        public AvailabilityService(BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;

            var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this._logger = logger;
        }

        public async Task<List<Availability>?> GetAvailabilitiesAsync(int userId)
        {
            if(this._bevakningContext == null || this._bevakningContext.Availabilities == null)
            {
                return null;
            }

            return await this._bevakningContext.Availabilities.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
