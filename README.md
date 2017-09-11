# OpenExchangeRatesClient

[OpenExchangeRates](https://openexchangerates.org/) API client for .NET.

Nuget: [Meziantou.OpenExchangeRatesClient](https://www.nuget.org/packages/Meziantou.OpenExchangeRatesClient/)

````csharp
using (var client = new OpenExchangeRatesClient())
{
    client.AppId = apiKey; // TODO set your api key
	
    var info = await client.GetUsageAsync();
    Console.WriteLine(info.Usage.RequestsRemaining);
    Console.WriteLine(info.Plan.Name);
    
    var currencies = await client.GetCurrenciesAsync(showAlternative: true);
    var exchangeRates = await client.GetExchangeRatesAsync(showAlternative: true);
    var exchangeRates = await client.GetExchangeRatesAsync(new DateTime(2017, 01, 01), showAlternative: true);
}
````
