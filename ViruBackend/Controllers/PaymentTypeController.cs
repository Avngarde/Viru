using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViruBackend.Dto;
using ViruBackend.Models;


namespace ViruBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private DbContext db;

        public PaymentTypeController(DbContext db)
        {
            this.db = db;
        }

        [Route("GetPaymentTypes/{walletId:int}")]
        [HttpGet]
        public PaymentType[] GetPaymentsTypes(int walletId)
        {
            PaymentType[] paymentTypes = db.PaymentTypes.Where(
                paymentType => paymentType.WalletId == walletId)
                .ToArray();
            return paymentTypes;
        }

        [Route("CreatePaymentType")]
        [HttpPost]
        public async Task AddPayment(AddPaymentTypeDto paymentTypeDto)
        {
            PaymentType paymentType = new PaymentType()
            {
                Name = paymentTypeDto.Name,
                Color = paymentTypeDto.Color,
                WalletId = paymentTypeDto.WalletId
            };

            await db.PaymentTypes.AddAsync(paymentType);
            db.SaveChanges();
        }

        [Route("DeletePaymentType/{paymentTypeId:int}")]
        [HttpDelete]
        public void DeletePaymentType(int paymentTypeId)
        {
            PaymentType paymentType = db.PaymentTypes.Where(paymentType => paymentType.Id == paymentTypeId).First();
            db.PaymentTypes.Remove(paymentType);
            db.SaveChanges();
        }
    }
}
