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

        public UserAccount? GetByUsername(string username)
        {
            return this._users.FirstOrDefault(x => x.Username == username);
        }
    }
}
