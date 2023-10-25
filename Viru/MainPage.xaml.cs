using System.Collections.ObjectModel;
using Viru.Dto;
using Viru.Services;

namespace Viru;

public partial class MainPage : ContentPage
{
    private WalletService walletService = new();
    public WalletDto[] Wallets;

    public MainPage()
	{
		InitializeComponent();
	}

    private async void mainPageActionButton_Clicked(object sender, EventArgs e)
    {
		string name = await DisplayPromptAsync("Add wallet", "Wallet's name:", "Add");
        if (name == null || name.Trim() == "") return;
        await walletService.AddWallet(name);

        //Refresh walletsList
        Wallets = await walletService.GetAllWallets();
        walletsList.ItemsSource = Wallets;
    }

    protected override async void OnAppearing()
    {
        Wallets = await walletService.GetAllWallets();
        walletsList.ItemsSource = Wallets;
    }

    private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
    {
        MenuItem menuItem = sender as MenuItem;
        int id = (int)menuItem.CommandParameter;
        await walletService.DeleteWallet(id);
        Wallets = await walletService.GetAllWallets();
        walletsList.ItemsSource = Wallets;
    }

    private async void walletsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        WalletDto wallet = e.Item as WalletDto;
        await DisplayAlert("Tapped", $"Selected wallet: {wallet.Name}", "Okey");
    }
}

