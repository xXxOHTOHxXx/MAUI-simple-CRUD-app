using _253502.Garnik.Pages;

namespace _253502.Garnik
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AuthorsPage), typeof(AuthorsPage));
            Routing.RegisterRoute(nameof(BooksPage),typeof(BooksPage));
            Routing.RegisterRoute(nameof(AddOrUpdateAuthor), typeof(AddOrUpdateAuthor));
            Routing.RegisterRoute(nameof(AddOrUpdateBook), typeof(AddOrUpdateBook));
        }
    }
}
