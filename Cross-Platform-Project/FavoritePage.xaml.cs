using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Cross_Platform_Project;

public partial class FavoritePage : ContentPage
{
    public FavoritePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (AppState.CurrentAccount != null)
        {
            FavoritesList.ItemsSource = null;
            FavoritesList.ItemsSource = AppState.CurrentAccount.Favorite;
        }
    }
}