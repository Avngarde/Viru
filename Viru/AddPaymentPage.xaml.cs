using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Viru.Services;

namespace Viru;

public partial class AddPaymentPage : ContentPage
{
    private int walletId;
    private PaymentService paymentService = new();
    private bool isExpense = false;
	private float value = 0;
	private string description = "";

    public AddPaymentPage(int walletId)
	{
		InitializeComponent();
        this.walletId = walletId;
	}

    private async void exitButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void descriptionEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        description = e.NewTextValue;
    }

    private void valueEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (e.NewTextValue == "") value = 0;
            else value = float.Parse(e.NewTextValue, CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            return;
        }
    }

    private void typeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        isExpense = e.Value;
    }

    private bool InputsCorrect()
    {
        if (description == "" && value == 0)
        {
            descriptionFrame.BorderColor = Color.FromArgb("#D62828");
            valueFrame.BorderColor = Color.FromArgb("#D62828");
            return false;
        }
        else if (description != "" && value == 0)
        {
            descriptionFrame.BorderColor = Color.FromArgb("#C8C8C8");
            valueFrame.BorderColor = Color.FromArgb("#D62828");
            return false;
        }
        else if (value != 0 && description == "")
        {
            descriptionFrame.BorderColor = Color.FromArgb("#D62828");
            valueFrame.BorderColor = Color.FromArgb("#C8C8C8");
            return false;
        }

        descriptionFrame.BorderColor = Color.FromArgb("#C8C8C8");
        valueFrame.BorderColor = Color.FromArgb("#C8C8C8");
        return true;
    }

    private async void addPaymentButton_Clicked(object sender, EventArgs e)
    {
        if (InputsCorrect())
        {
            value = Math.Abs(value);
            if (isExpense) value = value - (value * 2);
            await paymentService.AddPayment(description, "", value, walletId);
            await DisplayAlert("Success", "Payment added!", "Ok");
            await Navigation.PopModalAsync();
        }
    }
}