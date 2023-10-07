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
    }

    protected override async void OnAppearing()
    {
        Wallets = await walletService.GetAllWallets();
        walletsList.ItemsSource = Wallets;
    }
}

