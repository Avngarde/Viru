using static Viru.PaymentPage;

namespace Viru;

public partial class PaymentsFullListPage : ContentPage
{
	private List<PaymentListModel> paymentLists;
		
	public PaymentsFullListPage(List<PaymentListModel> payments)
	{
        this.paymentLists = payments;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        payments.ItemsSource = paymentLists;
    }

    private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}