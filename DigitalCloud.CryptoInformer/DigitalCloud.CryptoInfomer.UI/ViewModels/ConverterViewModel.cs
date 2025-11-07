using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Helpers.Constants;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Charts;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Currency;
using DigitalCloud.CryptoInformer.Application.Models.Response.Dropdowns;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.Globalization;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels
{
    //TODO make upload of next pfrt of coins for dropdown after getting to the end of th list
    //+ make posibility for searching of currency in dropdown
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



        [ObservableProperty] private PlotModel? _priceLinePlotModel;

        [ObservableProperty] private PlotModel? _candlePlotModel;


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

            if (getCoinListForDropdownOrError.IsError) 
            {
                //TODO make error visualization
                return;
            }

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
            if (string.IsNullOrWhiteSpace(value) || _сoinListForDropdown.Count == 0)
            {
                //TODO make error visualization
                return;
            }

            FromCurrencyCurrentCoin = _сoinListForDropdown.FirstOrDefault(c => c.Id == value);
            _ = LoadPriceChartAsync();

            // TODO: rebuild ToCoins list without FromCurrencyCurrentCoin.Id

            Result = null;

            ConvertAsyncCommand.NotifyCanExecuteChanged();
        }
                
        partial void OnSelectedToIdChanged(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            ToCurrencyCurrentCoin = _сoinListForDropdown.FirstOrDefault(c => c.Id == value);

            // TODO: rebuild FromCoins list without FromCurrencyCurrentCoin.Id

            Result = null;

            ConvertAsyncCommand.NotifyCanExecuteChanged();
        }

        
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
            var request = new GetDataForListChartRequest(
                                CoinId: FromCurrencyCurrentCoin!.Id,
                                VsCurrency: MarketCurrencies.USD,
                                Days: "7",
                                MarketChartInterval: null,
                                CurrencyPricePrecision: CurrencyPricePrecision.FULL
                            );

            var dataForListChartOrError = await _coinGeckoClient.GetDataForListChartAsync(request);

            if (dataForListChartOrError.IsError || dataForListChartOrError.Value is null)
            {
                //TODO make error visualization
                return;
            }

            var data = dataForListChartOrError.Value.Prices;

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

        [RelayCommand]
        public async Task LoadCandlesAsync()
        {
            if (FromCurrencyCurrentCoin is null)
            {
                //TODO make error visualization
                return;
            }

            var request = new GetDataForCandlestickChartRequest(
                                    FromCurrencyCurrentCoin.Id, 
                                    MarketCurrencies.USD, 
                                    DaysPeriod.THIRTY_DAYS, 
                                    CurrencyPricePrecision.FULL);

            var dataForCandleChartOrError = await _coinGeckoClient.GetDataForCandlestickChartAsync(request);

            if (dataForCandleChartOrError.IsError)
            {
                //TODO make error visualization
                return;
            }

            var candles = dataForCandleChartOrError.Value;  

            CandlePlotModel = null;

            var model = new PlotModel();

            model.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "MM-dd",
                IntervalType = DateTimeIntervalType.Days,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = MarketCurrencies.USD.ToUpperInvariant(),
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });

            var series = new CandleStickSeries
            {
                CandleWidth = 0.6
            };

            foreach (var c in candles)
            {
                var x = DateTimeAxis.ToDouble(
                    DateTimeOffset.FromUnixTimeMilliseconds(c.TimestampMs).UtcDateTime);

                series.Items.Add(new HighLowItem(
                                     x,
                                     (double)c.High,
                                     (double)c.Low,
                                     (double)c.Open,
                                     (double)c.Close));

            }

            model.Series.Add(series);

            CandlePlotModel = model;
        }
        partial void OnFromCurrencyCurrentCoinChanged(GetCoinsListForDropdawnResponse? value)
        {
            _ = LoadPriceChartAsync();

            _ = LoadCandlesAsync();
        }
    }
}

