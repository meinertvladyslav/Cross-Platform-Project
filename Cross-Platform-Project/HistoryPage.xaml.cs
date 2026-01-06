using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Cross_Platform_Project;

public partial class HistoryPage : ContentPage
{
    public HistoryPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (AppState.CurrentAccount != null)
        {
            HistoryList.ItemsSource = null;
            HistoryList.ItemsSource = AppState.CurrentAccount.History;
        }
    }
}