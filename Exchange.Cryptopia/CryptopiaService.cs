﻿using Exchange.Core.Interfaces;
using Exchange.Core.Models;
using Exchange.Cryptopia.APIResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Cryptopia
{
    public interface ICryptopiaService
    {
        Task<CryptopiaOrderBook> GetMarketOrdersAsync(string marketName);
        Task<ICurrencyCoin> Get24hrAsync(string symbol);
        Task<IEnumerable<ICurrencyCoin>> GetMarketsAsync();
        Task<IEnumerable<CryptopiaOrderBook>> GetMarketOrderGroupsAsync(string[] marketNames);
    }
    public class CryptopiaService : ICryptopiaService
    {
        private readonly IApiService _cryptopiaClient;

        public CryptopiaService()
        {
            _cryptopiaClient = new ApiService("https://www.cryptopia.co.nz/api/");
        }
        public async Task<ICurrencyCoin> Get24hrAsync(string symbol)
        {
            var result = await _cryptopiaClient.GetAsync<GetMarketResults>("GetMarket/" + symbol);

            if (result == null)
            {
                throw new NullReferenceException();
            }

            return result.Data;
        }

        public async Task<IEnumerable<ICurrencyCoin>> GetMarketsAsync()
        {
            var result = await _cryptopiaClient.GetAsync<GetMarketsResult>("GetMarkets");

            if (result == null)
            {
                throw new NullReferenceException();
            }

            return result.Data;
        }

        public async Task<CryptopiaOrderBook> GetMarketOrdersAsync(string marketName)
        {
            var result = await _cryptopiaClient.GetAsync<GetMarketOrderResults>(string.Format("GetMarketOrders/{0}", marketName));

            if (result == null)
            {
                throw new NullReferenceException();
            }

            return result.Data;
        }

        public async Task<IEnumerable<CryptopiaOrderBook>> GetMarketOrderGroupsAsync(string[] marketNames)
        {
            var result = await _cryptopiaClient.GetAsync<GetMarketOrderGroupsResults>(string.Format("GetMarketOrderGroups/{0}", string.Join('-', marketNames)));

            if (result == null)
            {
                throw new NullReferenceException();
            }

            return result.Data;
        }
    }
}