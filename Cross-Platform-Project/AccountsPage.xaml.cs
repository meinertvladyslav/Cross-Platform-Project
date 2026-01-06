using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Cross_Platform_Project;

public partial class AccountsPage : ContentPage
{
    public AccountsPage()
    {
        InitializeComponent();
        Accounts.Clear();
        foreach (var acc in AppState.Accounts)
            Accounts.Add(acc);

        BindingContext = this;
    }

    public ObservableCollection<Account> Accounts { get; }
    = new ObservableCollection<Account>();

    async void OnAccountSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Account selected)
        {
            AppState.CurrentAccount = selected;
            AccountStorage.SaveAccounts(AppState.Accounts);
            await Shell.Current.GoToAsync("..");
        }
    }

    async void CreateAccountClicked(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("Create account", "Enter name");
        if (string.IsNullOrWhiteSpace(name)) return;

        var account = new Account { Name = name };

        
        AppState.Accounts.Add(account);

       
        Accounts.Add(account);

       
        AppState.CurrentAccount = account;

        
        AccountStorage.SaveAccounts(AppState.Accounts);

        
        await Shell.Current.GoToAsync("..");
    }
}