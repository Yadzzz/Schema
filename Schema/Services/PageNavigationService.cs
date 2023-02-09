namespace Schema.Services
{
    public class PageNavigationService
    {
        private string activePage = string.Empty;

        public void UpdateActivePage(string page)
        {
            this.activePage = page;
        }

        public bool IsPageActive(string page)
        {
            return this.activePage == page;
        }

        public string ActivePage
        {
            get
            {
                return this.activePage;
            }
        }
    }
}
