using Viru.Dto;
using Viru.Services;

namespace Viru;

public partial class PaymentPage : ContentPage
{
	private int WalletId;
	public List<PaymentListModel> paymentList = new();
	public PaymentDto[] paymentsArray;
    private PaymentService paymentService = new PaymentService();
	private PaymentTypeService paymentTypeService = new();
	private bool isAddPageOpen = false;
	private int selectedYear = DateTime.Now.Year;
	private int selectedMonth = DateTime.Now.Month;

	private Dictionary<int, string> monthNumberName = new Dictionary<int, string>()
	{
		{1, "January"},
		{2, "February"},
		{3, "March"},
		{4, "April"},
		{5, "May"},
		{6, "June"},
		{7, "July"},
		{8, "August"},
		{9, "September"},
		{10, "October"},
		{11, "November"},
		{12, "December"}
	};
	public string selectedDateString = "";
	private List<int> availableYears = new();
	private List<int> availableMonths = new();

    public PaymentPage(WalletsListModel wallet)
	{
		Title = wallet.Name;
		WalletId = wallet.Id;
		InitializeComponent();

		selectedDateString = $"{monthNumberName[selectedMonth]} {selectedYear}";
		dateSelected.Text = selectedDateString;
        TitleBarText.Text = Title;
    }

    protected override async void OnAppearing()
    {
		paymentList = await GetPayments();
		payments.ItemsSource = paymentList;
    }

	private void SetAvailableDates(PaymentDto[] payments)
	{
		availableYears.Add(selectedYear);
		availableMonths.Add(selectedMonth);
		foreach(PaymentDto payment in payments)
		{
			if (!availableYears.Contains(payment.Created.Year))
				availableYears.Add(payment.Created.Year);
			if (!availableMonths.Contains(payment.Created.Month))
				availableMonths.Add(payment.Created.Month);
		}
	}

	private async Task<List<PaymentListModel>> GetPayments()
	{
		List<PaymentListModel> paymentTemp = new();
		PaymentDto[] payments = await paymentService.GetPayments(WalletId);
		SetAvailableDates(payments);
		payments = payments.Where(x => x.Created.Month == selectedMonth && x.Created.Year == selectedYear).ToArray();
		paymentsArray = payments;
		foreach (PaymentDto payment in payments)
		{
			string paymentTypeColorRgb = await paymentTypeService.GetPaymentColorById(WalletId, payment.PaymentTypeId);

            paymentTemp.Add(
				new PaymentListModel()
				{
					Id = payment.Id,
					PaymentColor = payment.Value < 0 ? Color.FromArgb("#D62828") : Color.FromArgb("#2e8b57"),
					Description = payment.Description,
					PaymentCurrency = payment.Value > 0 ? $"+{payment.Value} {payment.Currency}" : $"{payment.Value} {payment.Currency}",
					PaymentTypeColor = Color.FromArgb(paymentTypeColorRgb)
                }
			);
		}

		SummarizePayments(payments);

		if (payments.Length > 0)
			listRowDefinition.Height = new GridLength(25, GridUnitType.Star);
		else 
			listRowDefinition.Height = new GridLength(5, GridUnitType.Star);

		return paymentTemp;
	}

    private async void addPaymentButton_Clicked(object sender, EventArgs e)
    {
        isAddPageOpen = true;
		await Navigation.PushModalAsync(new AddPaymentPage(WalletId), true);
		isAddPageOpen = false;
        paymentList = await GetPayments();
        payments.ItemsSource = paymentList;
    }

	private void SummarizePayments(PaymentDto[] payments)
	{
		float sum = 0;
		foreach (PaymentDto payment in payments)
		{
			sum += payment.Value;
		}

		if (sum >= 0) 
		{
			summaryLabel.Text = $"+{sum}";
			summaryLabel.TextColor = Color.FromArgb("#31E981");
		}
		else
		{
            summaryLabel.Text = $"{sum}";
            summaryLabel.TextColor = Color.FromArgb("#FF0000");
        }
	}

    private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
    {
        MenuItem menuItem = sender as MenuItem;
        int id = (int)menuItem.CommandParameter;
        await paymentService.DeletePayment(id);
        OnAppearing();
		await DisplayAlert("Success", "Payment deleted succesfully", "Ok");
    }

    public class PaymentListModel
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string PaymentCurrency { get; set; }
		public Color PaymentTypeColor { get; set; }
        public Color PaymentColor { get; set; }
	}

    private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
		await Navigation.PushModalAsync(new PaymentsFullListPage(paymentList), true);
    }

	private async void previousDateButton_Clicked(object sender, EventArgs e)
	{
		int yearTemp = selectedYear;
		int monthTemp = selectedMonth;
		if (selectedMonth == 1)
		{
			selectedMonth = 12;
			selectedYear -= 1;
		}
		else
		{
			selectedMonth -= 1;
		}
		if (!availableYears.Contains(selectedYear) || !availableMonths.Contains(selectedMonth))
		{
			selectedYear = yearTemp;
			selectedMonth = monthTemp;
			return;
		}
		selectedDateString = $"{monthNumberName[selectedMonth]} {selectedYear}";
		dateSelected.Text = selectedDateString;
		OnAppearing();
	}

	private async void nextDateButton_Clicked(object sender, EventArgs e)
	{
		int yearTemp = selectedYear;
		int monthTemp = selectedMonth;
		if (selectedMonth == 12)
		{
			selectedMonth = 1;
			selectedYear += 1;
		}
		else 
		{
			selectedMonth += 1;
		}
		if (!availableYears.Contains(selectedYear) || !availableMonths.Contains(selectedMonth))
		{
			selectedYear = yearTemp;
			selectedMonth = monthTemp;
			return;
		}
		selectedDateString = $"{monthNumberName[selectedMonth]} {selectedYear}";
		dateSelected.Text = selectedDateString;
		OnAppearing();
	}

	private async void chartsPageButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new WalletCharts(WalletId, this.Title, availableYears, availableMonths, paymentsArray));
	}
}
