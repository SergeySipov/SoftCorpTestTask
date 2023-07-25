namespace Application.Common.Models.CurrencyExchange;

public class CurrencyConverterResponseModel
{
    public string date { get; set; }
    public string historical { get; set; }
    public Info info { get; set; }
    public Query query { get; set; }
    public double result { get; set; }
    public bool success { get; set; }
}