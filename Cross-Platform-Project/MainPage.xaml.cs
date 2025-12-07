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
        string json;
        //creating variable movie list with Movies class inside
        List<Movies> movieList = new List<Movies>();
        //helps with downloading file
        HttpClient client = new HttpClient();

        private async void LoadMovies()
        {
            try
            {
                //checking if file exists, if it doesnt, create it 
                if (!File.Exists(localCache))
                {

                    //created string json using server client to get response from website(url) in string format
                    json = await client.GetStringAsync(url);

                    File.WriteAllText(localCache, json);


                }
                else
                {
                    json = File.ReadAllText(localCache);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("ERROR, Problem detected", ex.Message);
            }
        }
        private void Looking(object sender, TextChangedEventArgs e)
        {

        }



        public MainPage()
        {
            InitializeComponent();

        }

       
    }
}
