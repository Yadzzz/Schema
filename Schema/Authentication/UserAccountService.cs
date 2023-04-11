using DataAccessLibrary.Context;

namespace Schema.Authentication
{
    public class UserAccountService
    {
        private List<UserAccount> _users;

        public UserAccountService()
        {
            this._users = new List<UserAccount>
            {
                new UserAccount { Username = "yad", Password = "123", Role = "Administrator" },
                new UserAccount { Username = "amad", Password = "12345", Role = "User" }
            };
        }

        public async Task<UserAccount?> AuthenticateUser(string username, string password)
        {
            BevakningContext bevakningContext = new BevakningContext();

            if (bevakningContext == null || bevakningContext.Users == null)
                return null;

            var user = bevakningContext.Users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();

            if(user == null)
            {
                return null;
            }

            UserAccount userAccount = new()
            {
                Username = user.Username,
                Role = user.Role
            };

            return userAccount;
        }

        public UserAccount? GetByUsername(string username)
        {
            return this._users.FirstOrDefault(x => x.Username == username);
        }
    }
}
