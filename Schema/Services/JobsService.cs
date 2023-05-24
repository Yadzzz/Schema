using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Schema.Services
{
    public class JobsService
    {
        private Microsoft.Extensions.Logging.ILogger _logger { get; set; }
        private readonly DataAccessLibrary.Context.BevakningContext _bevakningContext;

        public JobsService(DataAccessLibrary.Context.BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;

            var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this._logger = logger;
        }

        public async Task<List<DataAccessLibrary.Models.Job>> GetJobsAsync()
        {
            if (this._bevakningContext == null || this._bevakningContext.Jobs == null)
                return null;

            try
            {
                return await this._bevakningContext.Jobs.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<bool> UpdateJob(DataAccessLibrary.Models.Job job)
        {
            if (job == null || this._bevakningContext == null || this._bevakningContext.Jobs == null) return false;

            try
            {
                this._bevakningContext.Jobs.Update(job);
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

        public async Task<bool> AddJob(DataAccessLibrary.Models.Job job)
        {
            if (job == null || this._bevakningContext == null || this._bevakningContext.Jobs == null) return false;

            try
            {
                await this._bevakningContext.Jobs.AddAsync(job);
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

        public async Task<bool> RemoveJob(DataAccessLibrary.Models.Job job)
        {
            if (job == null || this._bevakningContext == null || this._bevakningContext.Jobs == null) return false;

            try
            {
                this._bevakningContext.Jobs.Remove(job);
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
