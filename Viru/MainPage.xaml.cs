using Viru.Dto;
using Viru.Services;

namespace Viru;

public partial class MainPage : ContentPage
{
    private WalletService walletService = new();

    public MainPage()
	{
		InitializeComponent();
	}

    private async void mainPageActionButton_Clicked(object sender, EventArgs e)
    {
        WalletDto[] wallets = await walletService.GetAllWallets();
		string name = await DisplayPromptAsync("Add wallet", "Wallet's name:", "Add");
    }
}

