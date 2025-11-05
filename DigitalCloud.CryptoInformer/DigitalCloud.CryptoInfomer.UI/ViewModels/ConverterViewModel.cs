using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Helpers.Constants;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.ObjectModel;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels
{
    public partial class ConverterViewModel : ObservableObject
    {
        private readonly ICoinGeckoClient _coinGeckoClient;


        [ObservableProperty] private string? _fromCurrencyId;
        [ObservableProperty] private string? _toCurrencyId;

        [ObservableProperty]
        private PlotModel? _priceLinePlotModel;

        public ConverterViewModel(ICoinGeckoClient coinGeckoClient)
        {
            _coinGeckoClient = coinGeckoClient;

            LoadInitialCoinListCommand = new AsyncRelayCommand(LoadInitialCoinListAsync);

            _ = LoadInitialCoinListAsync();
        }


        public ObservableCollection<DropdownCoin> FromCoins { get; } = new();
        public ObservableCollection<DropdownCoin> ToCoins { get; } = new();


        public IAsyncRelayCommand LoadInitialCoinListCommand { get; }


        public sealed record DropdownCoin(string Id, string Display);


        private async Task LoadInitialCoinListAsync()
        {
            var res = await _coinGeckoClient.GetCoinsListForDropdawnAsync(
                new GetCoinsListForDropdawnRequest(250, 1));

            if (res.IsError) return;

            FromCoins.Clear();

            ToCoins.Clear();

            var all = res.Value
                .Select(x => new DropdownCoin(x.Id, $"{x.Name} ({x.Symbol.ToUpperInvariant()})"))
                .ToList();

            foreach (var c in all)
                FromCoins.Add(c);

            foreach (var c in all.Skip(1))
                ToCoins.Add(c);

            FromCurrencyId ??= FromCoins.FirstOrDefault()?.Id;
        }

        [RelayCommand]
        private async Task LoadPriceChartAsync()
        {
            var request = new GetMarketChartByIdRequest(
                                CoinId: FromCurrencyId!,
                                VsCurrency: MarketCurrencies.USD,
                                Days: "7",
                                MarketChartInterval: null,
                                CurrencyPricePrecision: CurrencyPricePrecision.FULL
                            );

            var response = await _coinGeckoClient.GetDataForMarketChart(request);

            if (response.IsError || response.Value is null)
                return;

            var data = response.Value.Prices;

            var model = new PlotModel();
            model.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "MM-dd",
                IntervalType = DateTimeIntervalType.Days
            });
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = MarketCurrencies.USD.ToUpperInvariant()
            });

            var series = new LineSeries { StrokeThickness = 2 };

            foreach (var p in data)
            {
                var tsMs = (long)p[0];
                var price = p[1];
                var x = DateTimeAxis.ToDouble(DateTimeOffset.FromUnixTimeMilliseconds(tsMs).UtcDateTime);
                series.Points.Add(new DataPoint(x, price));
            }

            model.Series.Add(series);
            PriceLinePlotModel = model;
        }

        partial void OnFromCurrencyIdChanged(string? value)
        {
            _ = LoadPriceChartAsync();
        }
    }
}

