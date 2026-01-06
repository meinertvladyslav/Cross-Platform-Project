namespace Cross_Platform_Project
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
            Routing.RegisterRoute(nameof(FavoritePage), typeof(FavoritePage));
            Routing.RegisterRoute(nameof(AccountsPage), typeof(AccountsPage));
        }

        async void OpenHistory(object sender, EventArgs e)
            => await Shell.Current.GoToAsync(nameof(HistoryPage));

        async void OpenFavorite(object sender, EventArgs e)
            => await Shell.Current.GoToAsync(nameof(FavoritePage));

        async void OpenAccounts(object sender, EventArgs e)
            => await Shell.Current.GoToAsync(nameof(AccountsPage));

        async void RandomMovieClicked(object sender, EventArgs e)
            => await DisplayAlert("Menu", "Random Movie", "OK");

        void ColorChange(object sender, EventArgs e)
        {
            Application.Current.UserAppTheme =
                Application.Current.UserAppTheme == AppTheme.Dark
                ? AppTheme.Light
                : AppTheme.Dark;
        }
    }
}


