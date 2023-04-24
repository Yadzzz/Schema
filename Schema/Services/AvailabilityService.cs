using DataAccessLibrary.Context;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Schema.Services
{
    public class AvailabilityService
    {
        private BevakningContext _bevakningContext;

        public AvailabilityService(BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;
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
