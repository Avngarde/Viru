using System.Collections.ObjectModel;
using Viru.Dto;
using Viru.Services;

namespace Viru;

public partial class WalletCharts : ContentPage
{
    private int WalletId;
    List<int> availableYears;
    List<int> availableMonths;
    PaymentDto[] payments;
    PaymentTypeDto[] paymentTypes;
    float incomeSum;
    float expensesSum;
    float total;

    bool expensesToggled = false;

    List<PaymentTypeSummaryModel> paymentTypesSummaries = new();

    public WalletCharts(int walletId, string walletName, List<int> availableYears, List<int> availableMonths, PaymentDto[] payments)
	{
        WalletId = walletId;
        Title = $"{walletName} summary";
        this.availableYears = availableYears;
        this.availableMonths = availableMonths;
        this.payments = payments;
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        await InitPaymentTypes();

        incomeSum = payments.Where(x => x.Value > 0).Sum(x => x.Value);
        expensesSum = payments.Where(x => x.Value < 0).Sum(x => x.Value);
        total = incomeSum - Math.Abs(expensesSum);
        incomeLabel.Text = $"+{incomeSum}";
        expenseLabel.Text = $"{expensesSum}";
        totalLabel.Text = total.ToString();

        SummarizeTypes();
    }

    private async Task InitPaymentTypes()
    {
        PaymentTypeService paymentTypeService = new();
        paymentTypes = await paymentTypeService.GetPaymentTypes(WalletId);
    }

    private void SummarizeTypes()
    {
        paymentTypesSummaries.Clear();
        summaryListView.ItemsSource = null;
        foreach(PaymentTypeDto type in paymentTypes)
        {
            float typePaymentsValueSum = 0;
            if (expensesToggled)
            {
                typePaymentsValueSum = payments
                    .Where(x => x.PaymentTypeId == type.Id && x.Value < 0)
                    .Sum(x => x.Value);
                
                paymentTypesSummaries.Add(new PaymentTypeSummaryModel() 
                { 
                    TypeName = type.Name,
                    SumPercent = Math.Abs(typePaymentsValueSum) / Math.Abs(expensesSum),
                    SumPercentLabel = $"{Math.Round((Math.Abs(typePaymentsValueSum) / Math.Abs(expensesSum))*100)}%",
                    TypeColor = Color.FromRgba(type.Color)
                });
            }
            else 
            {
                typePaymentsValueSum = payments
                    .Where(x => x.PaymentTypeId == type.Id && x.Value >= 0)
                    .Sum(x => x.Value); 

                paymentTypesSummaries.Add(new PaymentTypeSummaryModel() 
                { 
                    TypeName = type.Name,
                    SumPercent = typePaymentsValueSum / incomeSum,
                    SumPercentLabel = $"{Math.Round((typePaymentsValueSum / incomeSum)*100)}%",
                    TypeColor = Color.FromRgba(type.Color)
                });             
            }
        }
        paymentTypesSummaries = paymentTypesSummaries.OrderByDescending(type => type.SumPercent).ToList();
        summaryListView.ItemsSource = paymentTypesSummaries;    
    }

    private void typeSwitch_OnToggled(object sender, EventArgs e)
    {
        if (typeSwitch.IsToggled)
        {
            typeSwitch.ThumbColor = Colors.Red;
            expensesToggled = true;
        }
        else
        {
            typeSwitch.ThumbColor = Colors.Green;
            expensesToggled = false;
        }
        SummarizeTypes();
    }

    public class PaymentTypeSummaryModel
    {
		public string TypeName { get; set; }
        public string SumPercentLabel { get; set; }
		public float SumPercent { get; set; }
		public Color TypeColor { get; set; }
	}
}
