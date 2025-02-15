﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ViruBackend.Dto;
using ViruBackend.Models;

namespace ViruBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> _logger;
        private DbContext db;

        public WalletController(ILogger<WalletController> logger, DbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [Route("GetAllWallets")]
        [HttpGet]
        public Wallet[] GetAllWallets() 
        {
            Wallet[] wallets = db.Wallets.ToArray();
            return wallets;
        }

        [Route("GetWallet/{id:int}")]
        [HttpGet]
        public async Task<Wallet> GetWallet(int id)
        {
            Wallet? wallet = await db.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return new Wallet();
            }

            return wallet;
        }

        [Route("AddWallet")]
        [HttpPost]
        public async Task AddWallet([FromBody]AddWalletDto addWalletDto)
        {
            Wallet newWallet = new()
            {
                Name = addWalletDto.Name,
                Created = addWalletDto.Created,
                TotalBalance = addWalletDto.TotalBalance,
                Color = addWalletDto.Color,
            };
            await db.Wallets.AddAsync(newWallet);
            await db.SaveChangesAsync();
        }

        [Route("EditWallet/{id:int}")]
        [HttpPut]
        public async Task EditWallet([FromBody]AddWalletDto walletDto, int id)
        {
            Wallet? wallet = await db.Wallets.FindAsync(id);
            if (wallet != null)
            {
                wallet.TotalBalance = walletDto.TotalBalance; 
                wallet.Color = walletDto.Color;
                wallet.Name = walletDto.Name;
                db.Wallets.Update(wallet);
                await db.SaveChangesAsync();
            }
        }

        [Route("DeleteWallet/{id:int}")]
        [HttpDelete]
        public async Task DeleteWallet(int id)
        {
            Wallet? wallet = await db.Wallets.FindAsync(id);
            if (wallet != null)
            {
                db.Wallets.Remove(wallet);
                await db.SaveChangesAsync();
            }
        }
    }
}
