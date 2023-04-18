using Microsoft.AspNetCore.Components.Authorization;

namespace Schema.Services
{
    public class UserDataService
    {
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
        }

        public async Task InitializeUserData(string? username)
        {
            if (string.IsNullOrEmpty(username))
            {
                var loggedInUser = (Authentication.CustomAuthenticationStateProvider)authStateProvider;
                var state = await loggedInUser.GetAuthenticationStateAsync();
                username = state?.User?.Identity?.Name ?? "";

                if (string.IsNullOrEmpty(username))
                {
                    return;
                }
            }

            this.Username = username;

            this.user = await this.usersService.GetUser(username);
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
            if(this.user == null || this.bookings == null)
            {
                await this.InitializeUserData(this.Username);
            }
        }
    }
}
