using APBD_PROJEKT.Exceptions;
using Newtonsoft.Json;

namespace APBD_PROJEKT.Helpers.CurrencyHelpers;

public class Currency
{
    public static decimal GetCurrencyRate(string symbol)
    {
        var URLString = "https://v6.exchangerate-api.com/v6/9d3c1067e1082b293dcfd92a/latest/PLN";
        using (var webClient = new System.Net.WebClient())
        {
            var json = webClient.DownloadString(URLString);
            Console.WriteLine(json);
            var response = JsonConvert.DeserializeObject<ExchangeRateResponse>(json);
            
            if (response.ConversionRates.TryGetValue(symbol.ToUpper(), out var rate))
            {
                return rate;
            }
            
            throw new NoSuchCurrencyException($"There is no currency: {symbol.ToUpper()} in our storage");
        }
    }
}