using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Helpers.Constants;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.Globalization;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels
{
    public partial class ConverterViewModel : ObservableObject
    {
        private readonly ICoinGeckoClient _coinGeckoClient;


        private const decimal AMOUNT_MIN = 0.00000001m;
        private const decimal AMOUNT_MAX = 100_000m;


        private List<GetCoinsListForDropdawnResponse> _сoinListForDropdown = new();


        [ObservableProperty] private string? _selectedFromId;
        [ObservableProperty] private string? _selectedToId;


        [ObservableProperty] private GetCoinsListForDropdawnResponse? _fromCurrencyCurrentCoin;
        [ObservableProperty] private GetCoinsListForDropdawnResponse? _toCurrencyCurrentCoin;


        [ObservableProperty] private string _amount = "1";     
        [ObservableProperty] private bool _isAmountValid;
        [ObservableProperty] private decimal? _result;

        private static bool TryParseAmount(string? s, out decimal val) => decimal.TryParse(
                           s, NumberStyles.Number, CultureInfo.InvariantCulture, out val);


        private bool CanConvert() =>
                                 FromCurrencyCurrentCoin?.CurrentPrice is decimal &&
                                 ToCurrencyCurrentCoin?.CurrentPrice is decimal &&
                                 IsAmountValid;


        //[ObservableProperty] private decimal _amountFrom; 
        //[ObservableProperty] private decimal _amountTo;  


        [ObservableProperty]
        private PlotModel? _priceLinePlotModel;

        public ConverterViewModel(ICoinGeckoClient coinGeckoClient)
        {
            _coinGeckoClient = coinGeckoClient;

            LoadInitialCoinListCommand = new AsyncRelayCommand(LoadInitialCoinListAsync);
            ConvertAsyncCommand = new AsyncRelayCommand(ConvertAsync, CanConvert);


            _ = LoadInitialCoinListAsync();

            OnAmountChanged(Amount);
            ConvertAsyncCommand.NotifyCanExecuteChanged();
        }


        public ObservableCollection<DropdownCoin> FromCoins { get; } = new();
        public ObservableCollection<DropdownCoin> ToCoins { get; } = new();


        public IAsyncRelayCommand LoadInitialCoinListCommand { get; }


        public sealed record DropdownCoin(string Id, string Display);


        public IAsyncRelayCommand ConvertAsyncCommand { get; }



        //Load lists for dropdowns
        private async Task LoadInitialCoinListAsync()
        {
            var getCoinListForDropdownOrError = await _coinGeckoClient.GetCoinsListForDropdawnAsync(
                new GetCoinsListForDropdawnRequest(250, 1));

            if (getCoinListForDropdownOrError.IsError) return;

            _сoinListForDropdown = getCoinListForDropdownOrError.Value;

            FromCoins.Clear();

            ToCoins.Clear();

            var all = _сoinListForDropdown
                .Select(x => new DropdownCoin(x.Id, $"{x.Name} ({x.Symbol.ToUpperInvariant()})"))
                .ToList();

            foreach (var c in all)
                FromCoins.Add(c);

            // TODO: add skipping of SelectedFromId after list rewrite logic is done
            foreach (var c in all)
                ToCoins.Add(c);

            SelectedFromId = _сoinListForDropdown[0].Id;

            
        }


        //Selected coin due to selection in dropdowns
        partial void OnSelectedFromIdChanged(string? value)
        {
            if (string.IsNullOrWhiteSpace(value) || _сoinListForDropdown.Count == 0) return;

            FromCurrencyCurrentCoin = _сoinListForDropdown.FirstOrDefault(c => c.Id == value);
            _ = LoadPriceChartAsync();

            // TODO: rebuild ToCoins list without FromCurrencyCurrentCoin.Id

            ConvertAsyncCommand.NotifyCanExecuteChanged();
        }
                
        partial void OnSelectedToIdChanged(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            ToCurrencyCurrentCoin = _сoinListForDropdown.FirstOrDefault(c => c.Id == value);

            // TODO: rebuild FromCoins list without FromCurrencyCurrentCoin.Id

            ConvertAsyncCommand.NotifyCanExecuteChanged();
        }

        //
        partial void OnAmountChanged(string value)
        {
            IsAmountValid = TryParseAmount(value, out var v) && v > AMOUNT_MIN && v < AMOUNT_MAX;
            ConvertAsyncCommand.NotifyCanExecuteChanged();
        }

        //Convert Btn
        [RelayCommand(CanExecute = nameof(CanConvert))]
        private Task ConvertAsync()
        {
            if (!TryParseAmount(Amount, out var amt)) return Task.CompletedTask;

            var fp = FromCurrencyCurrentCoin?.CurrentPrice;
            var tp = ToCurrencyCurrentCoin?.CurrentPrice;

            if (fp is null || tp is null || tp == 0m) { Result = null; return Task.CompletedTask; }

            Result = Math.Round(amt * (fp.Value / tp.Value), 8);
            return Task.CompletedTask;
        }

        //Load line chart
        [RelayCommand]
        public async Task LoadPriceChartAsync()
        {
            var request = new GetMarketChartByIdRequest(
                                CoinId: FromCurrencyCurrentCoin!.Id,
                                VsCurrency: MarketCurrencies.USD,
                                Days: "7",
                                MarketChartInterval: null,
                                CurrencyPricePrecision: CurrencyPricePrecision.FULL
                            );

            var response = await _coinGeckoClient.GetDataForMarketChart(request);

            if (response.IsError || response.Value is null)
                return;

            var data = response.Value.Prices;

            PriceLinePlotModel = null;

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

        partial void OnFromCurrencyCurrentCoinChanged(GetCoinsListForDropdawnResponse? value)
        {
            _ = LoadPriceChartAsync();
        }
    }
}

