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
        string text;
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
                    //writing everything here
                    File.WriteAllText(localCache, json);


                }
                else
                {
                    //stores everything here
                    json = File.ReadAllText(localCache);
                }
            }
            //if something happend it will let me know
            catch (Exception ex)
            {
                Console.WriteLine("ERROR, Problem detected", ex.Message);
            }
        }
        //method that helps me with searching
        private void Looking(object sender, TextChangedEventArgs e)
        {
            //making everything uppercase, so its wouldnt be case sensetive
            text = e.NewTextValue.ToUpper();
            if (String.IsNullOrEmpty(text))
            {

            }
        }
        private void ListOfMovies_Tapped(object sender, EventArgs e)
        {

        }


        public MainPage()
        {
            InitializeComponent();
            
        }

       
    }
}
