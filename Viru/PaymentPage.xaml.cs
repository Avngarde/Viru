using Viru.Dto;
using Viru.Services;

namespace Viru;

public partial class PaymentPage : ContentPage
{
	private int WalletId;
	public List<PaymentListModel> paymentList = new();
    private PaymentService paymentService = new PaymentService();

    public PaymentPage(WalletDto wallet)
	{
		Title = wallet.Name;
		WalletId = wallet.Id;
		InitializeComponent();
        TitleBarText.Text = Title;
    }

    protected override async void OnAppearing()
    {
		paymentList = await GetPayments();
		payments.ItemsSource = paymentList;
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
					PaymentColor = payment.Value < 0 ? Color.FromArgb("#cc0000") : Color.FromArgb("#2e8b57"),
					Description = payment.Description,
					PaymentCurrency = $"{payment.Value} {payment.Currency}"
                }
			);
		}

		return paymentTemp;
	}

	public class PaymentListModel
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string PaymentCurrency { get; set; }
        public Color PaymentColor { get; set; }
	}
}
