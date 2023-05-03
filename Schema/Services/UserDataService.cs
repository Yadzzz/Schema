using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Serilog;

namespace Schema.Services
{
    public class UserDataService
    {
        //[Inject]
        private Microsoft.Extensions.Logging.ILogger _logger { get; set; }

        public string? Username { get; set; }
        private DataAccessLibrary.Models.User? user;
        private List<DataAccessLibrary.Models.Schedule>? bookings;

        private UsersService usersService;
        private BookingsService bookingsService;

        private AuthenticationStateProvider authStateProvider;

        public UserDataService(UsersService _usersService, BookingsService _bookingsService, AuthenticationStateProvider auth)
        {
            this.usersService = _usersService;
            this.bookingsService = _bookingsService;
            this.authStateProvider = auth;

            var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this._logger = logger;
        }

        public async Task InitializeUserData(string? username)
        {
            if (string.IsNullOrEmpty(username))
            {
                try
                {
                    var loggedInUser = (Authentication.CustomAuthenticationStateProvider)authStateProvider;
                    var state = await loggedInUser.GetAuthenticationStateAsync();
                    username = state?.User?.Identity?.Name ?? "";

                    if (string.IsNullOrEmpty(username))
                    {
                        return;
                    }
                }
                catch(Exception ex)
                {
                    await Console.Out.WriteLineAsync("Kunde inte InitializeUserData");
                    this._logger.LogError(ex.ToString());
                    return;
                }
            }

            this.Username = username;
            await Console.Out.WriteLineAsync(username);

            this.user = await this.usersService.GetUser(username);
            await Console.Out.WriteLineAsync(user.Id.ToString());
            if (this.user != null)
            {
                this.bookings = await this.bookingsService.GetBookingsForUser(this.user.Id);
            }
        }

        public DataAccessLibrary.Models.User? User
        {
            get 
            {
                return this.user; 
            }
        }

        public List<DataAccessLibrary.Models.Schedule>? Bookings
        {
            get
            {
                return this.bookings;
            }
        }


        public async Task LoadData()
        {
            if (this.user == null || this.bookings == null)
            {
                await this.InitializeUserData(this.Username);
            }
        }
    }
}
