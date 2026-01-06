using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Platform_Project;

public partial class AccountsPage : ContentPage
{
    List<Account> accounts;
    Action<Account> onSelected;

    public AccountsPage(List<Account> accounts, Action<Account> onSelected)
    {
        InitializeComponent();
        this.accounts = accounts;
        this.onSelected = onSelected;

        AccountsView.ItemsSource = accounts;
    }

    void AccountSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Account account)
        {
            onSelected(account);     
            Navigation.PopAsync();   
        }
    }

    async void AddAccountClicked(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync(
            "New account",
            "Enter account name");

        if (string.IsNullOrWhiteSpace(name))
            return;

        var account = new Account { Name = name };
        accounts.Add(account);

        AccountStorage.SaveAccounts(accounts);

        AccountsView.ItemsSource = null;
        AccountsView.ItemsSource = accounts;
    }
}