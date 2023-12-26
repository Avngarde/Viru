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

    public WalletCharts(int walletId, string walletName, List<int> availableYears, List<int> availableMonths, PaymentDto[] payments)
	{
        WalletId = walletId;
        Title = $"{walletName} summary";
        this.availableYears = availableYears;
        this.availableMonths = availableMonths;
        this.payments = payments;
		InitializeComponent();
    }
}
