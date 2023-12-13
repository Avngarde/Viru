﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Viru.Dto;

namespace Viru.Services
{
    public class PaymentTypeService
    {
        private HttpClient client;

        public PaymentTypeService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.0.142:5255");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PaymentTypeDto[]> GetPaymentTypes(int walletId)
        {
            using HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"PaymentType/GetPaymentTypes/{walletId}");
            Console.WriteLine("GetAll Status: " + response.StatusCode);
            return await response.Content.ReadFromJsonAsync<PaymentTypeDto[]>();
        }

        public async Task AddPaymentType(string name, string color, int walletId)
        {
            PaymentTypeDto paymentTypeDto = new()
            {
                Name = name,
                Color = color,
                WalletId = walletId
            };

            JsonContent jsonContent = JsonContent.Create(paymentTypeDto);
            using HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "PaymentType/CreatePaymentType",
                jsonContent);
            Console.WriteLine("AddPaymentType Status" + response.StatusCode);
        }

        public async Task DeletePayment(int id)
        {
            using HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress + $"PaymentType/DeletePaymentType/{id}");
            Console.WriteLine("DeletePaymentType Status" + response.StatusCode);
        }
    }
}
