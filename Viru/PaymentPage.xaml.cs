using Viru.Dto;

namespace Viru;

public partial class PaymentPage : ContentPage
{
	private int WalletId;

	public PaymentPage(WalletDto wallet)
	{
		Title = wallet.Name;
		WalletId = wallet.Id;
		InitializeComponent();
	}
}