using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Platform_Project
{
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage(List<Movies> favorites)
        {
            InitializeComponent();
            FavoritesList.ItemsSource = favorites;
        }
    }
}
