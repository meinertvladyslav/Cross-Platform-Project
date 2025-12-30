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
        List<Movies> historyMovies = new();
        List<Movies> favoriteMovies = new();
        //helps with downloading file
        HttpClient client = new HttpClient();
        
        

        private async Task LoadMovies()
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

        //method that helps me with searching
        private void Looking(object sender, TextChangedEventArgs e)
        {
            //making everything uppercase, so its wouldnt be case sensetive
            text = e.NewTextValue.ToUpper();
            if (String.IsNullOrEmpty(text))
            {
                ListOfMovies.ItemsSource = movieList;
                return;
            }

            //looking for values and making them to upper, so its wouldnt be case sensetive, "m" in this case is the movie that will be writen in the search bar
            search = movieList.Where(m => m.title.ToUpper().Contains(text) || m.director.ToUpper().Contains(text) || m.GenreText.ToUpper().Contains(text) ||m.year.ToString().Contains(text)).ToList();

            ListOfMovies.ItemsSource = search;

        }
        //keeps 1 movie only once, so its deletes duplicates and limits history for 20 movies
        void AddToHistory(Movies movie)
        {
            historyMovies.RemoveAll(m => m.Id == movie.Id);
            historyMovies.Insert(0, movie);

            if (historyMovies.Count > 20)
                historyMovies.RemoveAt(historyMovies.Count - 1);
        }
        
        private void ListOfMovies_Tapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Movies movie)
            {
                AddToHistory(movie);
            }
        }

        void SaveHistory()
        {
            File.WriteAllText(historyPath, JsonSerializer.Serialize(historyMovies));
        }

        void LoadHistory()
        {
            if (File.Exists(historyPath))
                historyMovies = JsonSerializer.Deserialize<List<Movies>>(File.ReadAllText(historyPath));
        }

        void ColorChange(object sender, EventArgs e)
        {
            if (Application.Current.UserAppTheme == AppTheme.Dark)
                Application.Current.UserAppTheme = AppTheme.Light;
            else
                Application.Current.UserAppTheme = AppTheme.Dark;
        }
        async void OpenHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage(historyMovies));
        }

        async void OpenFavorites(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavoritesPage(favoriteMovies));
        }

        public MainPage()
        {
            InitializeComponent();
            
            LoadMovies();
            
        }

       
    }
}
