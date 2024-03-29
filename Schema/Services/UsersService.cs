﻿using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Schema.Services
{
    public class UsersService
    {
        private Microsoft.Extensions.Logging.ILogger _logger { get; set; }
        private readonly DataAccessLibrary.Context.BevakningContext _bevakningContext;

        public UsersService(DataAccessLibrary.Context.BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;

            var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this._logger = logger;
        }

        public async Task<List<DataAccessLibrary.Models.User>?> GetUsers()
        {
            if (this._bevakningContext == null || this._bevakningContext.Users == null)
                return null;

            try
            {
                return await this._bevakningContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<DataAccessLibrary.Models.User?> GetUser(string username)
        {
            if (this._bevakningContext == null || this._bevakningContext.Users == null)
                return null;

            try
            {
                return await this._bevakningContext.Users.Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        public DataAccessLibrary.Models.User? GetUser(int userId)
        {
            if (this._bevakningContext == null || this._bevakningContext.Users == null)
                return null;

            try
            {
                return this._bevakningContext.Users.Find(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this._logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<bool> UpdateUser(DataAccessLibrary.Models.User user)
        {
            if (user == null || this._bevakningContext == null) return false;

            try
            {
                this._bevakningContext.Users.Update(user);
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

        public async Task<bool> AddUser(DataAccessLibrary.Models.User user)
        {
            if (user == null || this._bevakningContext == null) return false;

            try
            {
                await this._bevakningContext.Users.AddAsync(user);
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

        public async Task<bool> RemoveUser(DataAccessLibrary.Models.User user)
        {
            if (user == null || this._bevakningContext == null || this._bevakningContext.Users == null) return false;

            try
            {
                this._bevakningContext.Users.Remove(user);
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
