using DatChatBot.DataLayer.Records;
using System.Globalization;
using CsvHelper;

namespace DavChatBot.Services.ChatServices
{
    public class StockService : IStockService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StockService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetStockMessage(string baseUrl, string parameter, CancellationToken ct)
        {
            try
            {
                var url = $"{baseUrl}?s={parameter}&f=sd2t2ohlcv&h&e=csv";

                using var httpClient = _httpClientFactory.CreateClient();

                var response = await httpClient.GetStreamAsync(url, ct);

                using var reader = new StreamReader(response);
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

                if (!await csvReader.ReadAsync())
                    throw new Exception("Could not read the stock csv");

                var stockData = csvReader.GetRecord<StockData>();

                if (stockData == null || stockData.Close == "N/D")
                    throw new Exception("Data cannot be found for the stock code");

                return $"{stockData.Symbol} quote is ${stockData.Close} per share.";
            }
            catch (Exception e)
            {
                return $"Error trying to read stock for code {parameter}. {e.Message}";
            }
        }
    }
}

