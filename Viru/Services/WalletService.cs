using System;
using System.Collections.Generic;
using System.Drawing;
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

        public async Task AddWallet(string name, Microsoft.Maui.Graphics.Color color)
        {
            string colorHTML = color.ToRgbaHex();
            AddWalletDto addWalletDto = new AddWalletDto() { Name = name, Created = DateTime.UtcNow, Color = colorHTML, TotalBalance = 0 };

            JsonContent jsonContent = JsonContent.Create(addWalletDto);
            using HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "Wallet/AddWallet", 
                jsonContent);
            Console.WriteLine("AddWallet Status" + response.StatusCode);
        }

        public async Task EditWallet(WalletDto walletDto, int id)
        {
            JsonContent jsonContent = JsonContent.Create(walletDto);
            using HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"Wallet/EditWallet/{id}",
                jsonContent);
            Console.WriteLine("EditWallet Status" + response.StatusCode);
        }

        public async Task DeleteWallet(int id)
        {
            using HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress + $"Wallet/DeleteWallet/{id}");
            Console.WriteLine("DeleteWallet Status" + response.StatusCode);
        }
    }
}
