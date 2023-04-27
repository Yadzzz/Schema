using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using Schema.Authentication;
using Schema.Models;
using Serilog;

namespace Schema.Services
{
    public class SessionService
    {
        private Microsoft.Extensions.Logging.ILogger _logger { get; set; }

        private string? username { get; set; }
        private string? password { get; set; }

        private ProtectedSessionStorage storage;

        public SessionService(ProtectedSessionStorage _storage)
        {
            this.storage = _storage;

            var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this._logger = logger;
        }

        private async Task CheckUserCredentials()
        {
            if (storage == null)
            {
                return;
            }

            try
            {
                var result = await storage.GetAsync<string>("[{$USERNAME}]");
                if (result.Success)
                {
                    this.username = result.Value;
                }

                var resultPass = await storage.GetAsync<string>("[{$PASSWORD}]");
                if (resultPass.Success)
                {
                    this.password = resultPass.Value;
                }
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());

                this.username = "";
                this.password = "";
            }
        }

        public async Task<string?> GetUsername()
        {
            if (!string.IsNullOrEmpty(this.username))
            {
                return this.username;
            }

            await this.CheckUserCredentials();

            return this.username;
        }

        public async Task<string?> GetPassword()
        {
            if (!string.IsNullOrEmpty(this.password))
            {
                return this.password;
            }

            await this.CheckUserCredentials();

            return this.password;
        }

        public async Task AddUser(string username, string password)
        {
            if (this.storage == null)
            {
                return;
            }

            await this.storage.SetAsync("[{$USERNAME}]", username);
            await this.storage.SetAsync("[{$PASSWORD}]", password);
        }

        public async Task ForceLogin()
        {
            if(string.IsNullOrEmpty(this.username) || string.IsNullOrEmpty(this.password) || this.storage == null)
            {
                return;
            }

            AuthenticationStateProvider authStateProvider = new CustomAuthenticationStateProvider(this.storage);
            UserAccountService userAccountService = new UserAccountService();

            var user = await userAccountService.AuthenticateUser(this.username, this.password);
            if (user != null)
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(new UserSession
                {
                    Username = user.Username,
                    Role = user.Role
                });
            }
        }
    }
}
