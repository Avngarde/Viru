using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViruBackend.Dto;
using ViruBackend.Models;


namespace ViruBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private DbContext db;

        public PaymentController(DbContext db)
        {
            this.db = db;
        }

        [Route("GetPayments/{walletId:int}")]
        [HttpGet]
        public Payment[] GetWalletPayments(int walletId)
        {
            Payment[] payments = db.Payments.Where(
                payment => payment.WalletId == walletId)
                .ToArray();
            return payments;
        }

        [Route("AddPayment")]
        [HttpPost]
        public async Task AddPayment(AddPaymentDto paymentDto)
        {
            Payment payment = new Payment()
            {
                WalletId = paymentDto.WalletId,
                Currency = paymentDto.Currency,
                Description = paymentDto.Description,
                Value = paymentDto.Value,
                PaymentTypeId = paymentDto.PaymentTypeId,
                Wallet = db.Wallets.Where(wallet => wallet.Id == paymentDto.WalletId).First(),
            };

            await db.Payments.AddAsync(payment);
            db.SaveChanges();
        }

        [Route("DeletePayment/{paymentId:int}")]
        [HttpDelete]
        public void DeletePayment(int paymentId)
        {
            Payment payment = db.Payments.Where(payment => payment.Id == paymentId).First();
            db.Payments.Remove(payment);
            db.SaveChanges();
        }
    }
}
