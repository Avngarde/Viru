using System.Net.Http.Headers;
using System.Net.Http.Json;
using Viru.Dto;

namespace Viru.Services
{
    public class PaymentService
    {
        private HttpClient client;

        public PaymentService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.0.130:5255");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PaymentDto[]> GetPayments(int walletId)
        {
            using HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"Payment/GetPayments/{walletId}");
            Console.WriteLine("GetAll Status: " + response.StatusCode);
            return await response.Content.ReadFromJsonAsync<PaymentDto[]>();
        }

        public async Task AddPayment(string description, string currency, float value, int walletId, int paymentTypeId)
        {
            AddPaymentDto addPaymentDto = new AddPaymentDto()
            {
                Description = description,
                Currency = currency,
                Value = value,
                WalletId = walletId,
                PaymentTypeId = paymentTypeId,
                Created = DateTime.UtcNow
            };

            JsonContent jsonContent = JsonContent.Create(addPaymentDto);
            using HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "Payment/AddPayment",
                jsonContent);
            Console.WriteLine("AddWallet Status" + response.StatusCode);
        }

        public async Task DeletePayment(int id)
        {
            using HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress + $"Payment/DeletePayment/{id}");
            Console.WriteLine("DeleteWallet Status" + response.StatusCode);
        }
    }
}
