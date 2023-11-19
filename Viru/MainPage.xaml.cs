using System.Collections.ObjectModel;
using Viru.Dto;
using Viru.Services;

namespace Viru;

public partial class MainPage : ContentPage
{
    private WalletService walletService = new();
    public WalletsListModel[] Wallets;
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
        Wallets = await GetWallets();
        walletsList.ItemsSource = Wallets;
    }

    protected override async void OnAppearing()
    {
        if (!ifPaymentPageOpened && !ifAddWalletPageOpened)
        {
            Wallets = await GetWallets();
            walletsList.ItemsSource = Wallets;
            ifPaymentPageOpened = false;
        }
    }

    private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
    {
        MenuItem menuItem = sender as MenuItem;
        int id = (int)menuItem.CommandParameter;
        await walletService.DeleteWallet(id);
        Wallets = await GetWallets();
        walletsList.ItemsSource = Wallets;
    }

    private async void walletsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        WalletsListModel wallet = e.Item as WalletsListModel;
        PaymentPage paymentPage = new PaymentPage(wallet);
        ifPaymentPageOpened = true;
        await Navigation.PushAsync(paymentPage);
    }

    private async Task<WalletsListModel[]> GetWallets()
    {
        List<WalletsListModel> walletLists = new();
        WalletDto[] walletDtoList;
        walletDtoList = await walletService.GetAllWallets();
        foreach(WalletDto walletDto in walletDtoList)
        {
            walletLists.Add(new WalletsListModel()
            {
                Id = walletDto.Id,
                Name = walletDto.Name,
                Color = Color.FromArgb(walletDto.Color),
                TotalBalance = walletDto.TotalBalance,
            });
        }

        return walletLists.ToArray();
    }
}

public class WalletsListModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Color Color { get; set; }
    public float TotalBalance { get; set; }
}

