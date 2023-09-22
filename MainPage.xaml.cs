namespace Viru;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    private async void mainPageActionButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new AddWalletPage());
    }
}

