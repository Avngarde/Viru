using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Viru.Dto;

namespace Viru.Services
{
    public class WalletService
    {
        private HttpClient client;

        public WalletService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.1.202:5255");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<WalletDto[]> GetAllWallets()
        {
            using HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "Wallet/GetAllWallets");
            Console.WriteLine("GetAll Status: " + response.StatusCode);
            return await response.Content.ReadFromJsonAsync<WalletDto[]>();
        }
    }
}
