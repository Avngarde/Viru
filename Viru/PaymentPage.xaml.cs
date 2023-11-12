using Viru.Dto;
using Viru.Services;

namespace Viru;

public partial class PaymentPage : ContentPage
{
	private int WalletId;
	public List<PaymentListModel> paymentList = new();
    private PaymentService paymentService = new PaymentService();
	private bool isAddPageOpen = false;

    public PaymentPage(WalletDto wallet)
	{
		Title = wallet.Name;
		WalletId = wallet.Id;
		InitializeComponent();
        TitleBarText.Text = Title;
    }

    protected override async void OnAppearing()
    {
		if (!isAddPageOpen)
		{
			paymentList = await GetPayments();
			payments.ItemsSource = paymentList;
			isAddPageOpen = false;
		}
    }

	private async Task<List<PaymentListModel>> GetPayments()
	{
		List<PaymentListModel> paymentTemp = new();
		PaymentDto[] payments = await paymentService.GetPayments(WalletId);
		foreach (PaymentDto payment in payments)
		{
			paymentTemp.Add(
				new PaymentListModel()
				{
					PaymentColor = payment.Value < 0 ? Color.FromArgb("#D62828") : Color.FromArgb("#2e8b57"),
					Description = payment.Description,
					PaymentCurrency = $"{payment.Value} {payment.Currency}"
                }
			);
		}

		return paymentTemp;
	}

    private async void addPaymentButton_Clicked(object sender, EventArgs e)
    {
        isAddPageOpen = true;
        await Navigation.PushModalAsync(new AddPaymentPage(), true);
    }

    public class PaymentListModel
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string PaymentCurrency { get; set; }
        public Color PaymentColor { get; set; }
	}
}
