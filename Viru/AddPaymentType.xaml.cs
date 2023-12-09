using Viru.Services;

namespace Viru;

public partial class AddPaymentType : ContentPage
{
    private Color choosenColorVar = new Color(0, 0, 0);
    private PaymentTypeService paymentTypeService = new();
    private string typeName = "";
    private int walletId;

    public AddPaymentType(int walletId)
	{
		InitializeComponent();
        this.walletId = walletId;
	}

    private async void exitButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private void ColorPicker_PickedColorChanged(object sender, Color e)
    {
        choosenColor.BackgroundColor = e;
        choosenColorVar = e;
    }

    private void typeNameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        typeName = typeNameEntry.Text;
    }

    private void HideKeyboard()
    {
        typeNameEntry.IsEnabled = false;
    }

    private async void addTypeButton_Clicked(object sender, EventArgs e)
    {
        await paymentTypeService.AddPaymentType(typeName, choosenColorVar.ToRgbaHex(), walletId);
        HideKeyboard();
        await DisplayAlert("Success", $"Type {typeName} added!", "Ok");
        await Navigation.PopModalAsync();
    }
}