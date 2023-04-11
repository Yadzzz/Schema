using DataAccessLibrary.Context;

namespace Schema.Authentication
{
    public class UserAccountService
    {
        private BevakningContext _bevakningContext;
        private List<UserAccount> _users;

        public UserAccountService(BevakningContext bevakningContext)
        {
            this._bevakningContext = bevakningContext;

            this._users = new List<UserAccount>
            {
                new UserAccount { Username = "yad", Password = "123", Role = "Administrator" },
                new UserAccount { Username = "amad", Password = "12345", Role = "User" }
            };
        }

        public bool AuthenticateUser(string username, string password, out UserAccount userAccount)
        {
            userAccount = null;

            if (this._bevakningContext == null || this._bevakningContext.Users == null)
                return false;

            var user = this._bevakningContext.Users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();

            if(user == null)
            {
                return false;
            }

            userAccount = new()
            {
                Username = user.Username,
                Role = user.Role
            };

            return true;
        }

        public UserAccount? GetByUsername(string username)
        {
            return this._users.FirstOrDefault(x => x.Username == username);
        }
    }
}
