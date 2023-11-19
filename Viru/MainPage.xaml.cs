using System.Collections.ObjectModel;
using Viru.Dto;
using Viru.Services;

namespace Viru;

public partial class MainPage : ContentPage
{
    private WalletService walletService = new();
    public WalletDto[] Wallets;
    private bool ifPaymentPageOpened = false;
    private bool ifAddWalletPageOpened = false;

    public MainPage()
	{
		InitializeComponent();
    }

    private async void mainPageActionButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddWalletPage(), true);

        //Refresh walletsList
        Wallets = await walletService.GetAllWallets();
        walletsList.ItemsSource = Wallets;
    }

    protected override async void OnAppearing()
    {
        if (!ifPaymentPageOpened && !ifAddWalletPageOpened)
        {
            Wallets = await walletService.GetAllWallets();
            walletsList.ItemsSource = Wallets;
            ifPaymentPageOpened = false;
        }
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
        PaymentPage paymentPage = new PaymentPage(wallet);
        ifPaymentPageOpened = true;
        await Navigation.PushAsync(paymentPage);
    }
}

