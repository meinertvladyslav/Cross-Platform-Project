using System.ComponentModel.Design;
using System.Text.Json;
using System.Threading.Tasks;




namespace Cross_Platform_Project
{
    public partial class MainPage : ContentPage
    {
        //url to database
        string url = "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json";
        //Local cache will be saved here
        string localCache = Path.Combine(FileSystem.AppDataDirectory, "movieList.json");
        //to load info to and load from about history
        string historyPath = Path.Combine(FileSystem.AppDataDirectory, "history.json");
        string json;
        string text;
        //creating variable movie list with Movies class inside
        List<Movies> movieList = new List<Movies>();
        List<Movies> search = new List<Movies>();

        bool IsFavorite(Movies movie)
        {
            return currentAccount.Favorites.Any(m => m.Id == movie.Id);
        }

        List<Account> accounts;
        Account currentAccount;
        //helps with downloading file
        HttpClient client = new HttpClient();
        
        

        private async void LoadMovies()
        {

            //checking if file exists, if it doesnt, create it Console.WriteLine("MainPage constructor called");Console.WriteLine("MainPage constructor called");
            if (!File.Exists(localCache))
            {
                //created string json using server client to get response from website(url) in string format
                json = await client.GetStringAsync(url);
                //writing everything here
                File.WriteAllText(localCache, json);
            }
            else
            {
                //stores everything here
                json = File.ReadAllText(localCache);
            }
            movieList = JsonSerializer.Deserialize<List<Movies>>(json);

            ListOfMovies.ItemsSource = movieList;

        }
        //function for random movie feature
        async void ShowRandomMovie()
        {
           

            var random = new Random();
            var movie = movieList[random.Next(movieList.Count)];

            await DisplayAlert(
                "Random Movie",
                $"{movie.emoji} {movie.title} {movie.year}\n\n" +
                $"{movie.GenreText}",
                "Nice!"
            );

        }
        void RandomMovieClicked(object sender, EventArgs e)
        {
            ShowRandomMovie();
        }

        //method that helps me with searching
        private void Looking(object sender, TextChangedEventArgs e)
        {
            //making everything uppercase, so its wouldnt be case sensetive, and showing all of the movies, if nothing is writen
            text = e.NewTextValue.ToUpper();
            if (String.IsNullOrEmpty(text))
            {
                ListOfMovies.ItemsSource = movieList;
                return;
            }

            //looking for values and making them to upper, so its wouldnt be case sensetive, "m" in this case is the movie that will be writen in the search bar

            search = movieList.Where(m => m.title.ToUpper().Contains(text) || m.director.ToUpper().Contains(text) || m.GenreText.ToUpper().Contains(text) ||m.year.ToString().Contains(text) || m.rating.ToString().Contains(text) ).ToList();

            ListOfMovies.ItemsSource = search;
        
        }

        //keeps 1 movie only once, so its deletes duplicates and limits history for 20 movies
        void AddToHistory(Movies movie)
        {
            if (currentAccount == null)
                return;

            var history = currentAccount.History;

            history.RemoveAll(m => m.Id == movie.Id);
            history.Insert(0, movie);

            if (history.Count > 20)
                history.RemoveAt(history.Count - 1);

            AccountStorage.SaveAccounts(accounts);
        }
        //adding to history abd playing small animation
       async void ListOfMovies_Tapped(object sender, EventArgs e)
        {
            if (sender is Grid grid &&
        grid.BindingContext is Movies movie)
            {
                await grid.ScaleTo(0.9, 80);
                await grid.ScaleTo(1.05, 100);
                await grid.ScaleTo(1.0, 60);

                AddToHistory(movie);   
            }
        }


        //checking if there is already this movie was in favorite, if not, adds it
        void Favorite(Movies movie)
        {
            if (currentAccount == null)
                return;

            var favorites = currentAccount.Favorites;

            if (favorites.Any(m => m.Id == movie.Id))
            {
                favorites.RemoveAll(m => m.Id == movie.Id);
               
            }
            else
            {
                favorites.Add(movie);
                
            }

            AccountStorage.SaveAccounts(accounts);
        }
        //animation for the star button
        async void FavoriteClicked(object sender, EventArgs e)
        {
            if (sender is Button button &&
                button.BindingContext is Movies movie)
            {
                await button.RotateTo(360, 250);
                button.Rotation = 0;

                var favorites = currentAccount.Favorites;

                if (favorites.Any(m => m.Id == movie.Id))
                {
                    favorites.RemoveAll(m => m.Id == movie.Id);
                    button.Text = "☆";
                    button.TextColor = Colors.Gray;
                }
                else
                {
                    favorites.Add(movie);
                    button.Text = "★";
                    button.TextColor = Colors.Gold;
                }

                AccountStorage.SaveAccounts(accounts);
            }
        }
        //favorite movies is different for each account, so its making it not general for every account
        void FavoriteLoaded(object sender, EventArgs e)
        {
            if (sender is Button button &&
                button.BindingContext is Movies movie)
            {
                if (IsFavorite(movie))
                {
                    button.Text = "★";
                    button.TextColor = Colors.Gold;
                }
                else
                {
                    button.Text = "☆";
                    button.TextColor = Colors.Gray;
                }
            }
        }
        //loading accounts, and auto-selected first account is guest
        void LoadAccounts()
        {
            accounts = AccountStorage.LoadAccounts();

            if (accounts.Count == 0)
            {
                currentAccount = new Account { Name = "Guest" };
                accounts.Add(currentAccount);
                AccountStorage.SaveAccounts(accounts);
            }
            else
            {
                currentAccount = accounts[0]; 
            }

            Title = $"Welcome, {currentAccount.Name}";
        }
        //creates account
        async void CreateAccount()
        {
            string name = await DisplayPromptAsync(
                "Create account",
                "Enter account name");

            if (string.IsNullOrWhiteSpace(name))
                return;

            currentAccount = new Account { Name = name };
            accounts.Add(currentAccount);

            AccountStorage.SaveAccounts(accounts);
        }
        //opens and refresing accounts
        async void OpenAccounts(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new AccountsPage(accounts, account =>
                {
                    currentAccount = account;
                    Title = $"Welcome, {account.Name}";

                    
                    ListOfMovies.ItemsSource = null;
                    ListOfMovies.ItemsSource = movieList;
                }));
        }
        //Using AppThere changing colors from light to dark mode, white/dark
        void ColorChange(object sender, EventArgs e)
        {
            if (Application.Current.UserAppTheme == AppTheme.Dark)
                Application.Current.UserAppTheme = AppTheme.Light;
            else
                Application.Current.UserAppTheme = AppTheme.Dark;
        }

        //Navigations from main page to history/favorite

        async void OpenHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new HistoryPage(currentAccount.History));
        }

        async void OpenFavorites(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new FavoritesPage(currentAccount.Favorites));
        }



        //loading account before movies, so it would show information like history and favorite properly for each account
        public MainPage()
        {
            InitializeComponent();
            LoadAccounts();
            LoadMovies();

            
        }

       
    }
}
