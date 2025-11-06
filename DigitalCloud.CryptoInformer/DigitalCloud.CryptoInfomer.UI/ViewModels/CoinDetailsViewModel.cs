using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Currency;
using DigitalCloud.CryptoInformer.Application.Models.Response.Currency;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Diagnostics;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels;

public partial class CoinDetailsViewModel : ObservableObject
{
    private readonly ICoinGeckoClient _coinGeckoClient;


    [ObservableProperty]
    private GetCoinDetailsResponse? _coinDetails;


    [ObservableProperty]
    private GetCoinDetailsResponse? _coin;


    [ObservableProperty]
    private bool _isLoading;


    public CoinDetailsViewModel(ICoinGeckoClient coinGeckoClient)
    {
        _coinGeckoClient = coinGeckoClient;

        OpenTradeCommand = new RelayCommand<string?>(OpenTrade);
    }


    public IRelayCommand<string?> OpenTradeCommand { get; }


    public PlotModel PriceModel { get; } = new();


    partial void OnCoinChanged(GetCoinDetailsResponse? value)
    {
        BuildChart();
    }


    public async Task InitializeAsync(string coinId)
    {
        try
        {
            IsLoading = true;

            var coinDetailsRequest = new GetCoinDetailsRequest(
                                                    CoinId: coinId,
                                                    IncludeTickers: true,
                                                    IncludeLocalization: false,
                                                    IncludeMarketData: true,
                                                    IncludeCommunityData: false,
                                                    IncludeDeveloperData: false,
                                                    IncludeSparkline: true);

            var result = await _coinGeckoClient.GetDetailsInformationForCoinAsync(coinDetailsRequest);

            if (result.IsError)
            {

                return;
            }

            Coin = result.Value;
        }
        finally { IsLoading = false; }
    }


    private void OpenTrade(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return;

        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        catch
        {
            // error
        }
    }


    private void BuildChart()
    {
        var prices = Coin?.MarketData?.Sparkline7D?.Price; // List<decimal> з 168 значень
        PriceModel.Series.Clear();
        PriceModel.Axes.Clear();

        if (prices is null || prices.Count == 0)
        {
            PriceModel.InvalidatePlot(true);
            return;
        }

        var startUtc = DateTimeOffset.UtcNow.AddDays(-7);

        var series = new LineSeries { MarkerType = MarkerType.None };
        for (int i = 0; i < prices.Count; i++)
        {
            var tLocal = startUtc.AddHours(i).ToLocalTime().DateTime;  // або залишай .UtcDateTime
            series.Points.Add(DateTimeAxis.CreateDataPoint(tLocal, (double)prices[i]));
        }

        PriceModel.Axes.Add(new DateTimeAxis
        {
            Position = AxisPosition.Bottom,
            StringFormat = "dd.MM HH:mm",
            IntervalType = DateTimeIntervalType.Hours,   // підпис під годинний крок
            MajorGridlineStyle = LineStyle.None,
            MinorGridlineStyle = LineStyle.None
        });

        PriceModel.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            StringFormat = "#,0",
            MajorGridlineStyle = LineStyle.None,
            MinorGridlineStyle = LineStyle.None
        });

        PriceModel.Series.Add(series);
        PriceModel.InvalidatePlot(true);
    }
}
