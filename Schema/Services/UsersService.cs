﻿using Microsoft.EntityFrameworkCore;

namespace Schema.Services
{
    public class UsersService
    {
        private readonly DataAccessLibrary.Context.BevakningContext _bevakningContext;

        public UsersService(DataAccessLibrary.Context.BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
                return false;
            }
        }
    }
}