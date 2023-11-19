using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Viru.Services;

namespace Viru;

public partial class AddWalletPage : ContentPage
{
    private string walletName = "";
    private Color choosenColorVar = new Color(0, 0, 0);
    private WalletService walletService = new();

    public AddWalletPage()
    {
        InitializeComponent();
    }

    private async void exitButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void walletNameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        walletName = e.NewTextValue;
    }

    private void ColorPicker_PickedColorChanged(object sender, Color e)
    {
        choosenColor.BackgroundColor = e;
        choosenColorVar = e;
    }

    private void HideKeyboard()
    {
        walletNameEntry.IsEnabled = false;
    }

    private async void addWalletButton_Clicked(object sender, EventArgs e)
    {
        if (walletName == "")
        {
            walletNameFrame.BorderColor = Color.FromArgb("#D62828");
        }
        else
        {
            await walletService.AddWallet(walletName, choosenColorVar);
            HideKeyboard();
            await DisplayAlert("Success", $"Wallet {walletName} added!", "Ok");
            await Navigation.PopModalAsync();
        }
    }
}