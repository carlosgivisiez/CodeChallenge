using CodeChallenge.Chatroom.Core;
using CsvHelper;
using Flurl;
using Flurl.Http;
using System.Globalization;

namespace CodeChallenge.Chatroom.Bot.StockMarket
{
    public class StockMarketService : IChatbotService
    {
        private readonly Guid Id = new("ad4b1fcc-bb37-45f3-89b4-030a5da46ef8");
        private readonly string apiBaseUrl = "https://stooq.com";

        public async Task<Message?> Awnser(Message message, Room room)
        {
            var messageSplitted = message.Content.Split(" ");

            if (messageSplitted.FirstOrDefault() != "/stock")
            {
                return null;
            }

            var stockCode = messageSplitted.Skip(1).FirstOrDefault()?.ToUpper();

            if (string.IsNullOrEmpty(stockCode))
            {                
                return new Message("Did you forget the stock code?!", Id, message.Id);
            }

            var stockInfo = await GetStockInfo(stockCode);

            if (stockInfo == null)
            {
                return new Message($"Sorry. I didn't find any information about {stockCode}", Id, message.Id);
            }

            return new Message(stockInfo.ToString(), Id, message.Id);
        }

        private async Task<StockInfo?> GetStockInfo(string stockCode)
        {
            var csv = await apiBaseUrl
                .AppendPathSegments("q", "l")
                .SetQueryParam("f", "sd2t2ohlcv")
                .SetQueryParam("h")
                .SetQueryParam("e", "csv")
                .SetQueryParam("s", stockCode)
                .GetStreamAsync();

            using var reader = new StreamReader(csv);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var stockInfo = csvReader.GetRecords<StockInfo>();

            return stockInfo?.FirstOrDefault();
        }
    }
}
