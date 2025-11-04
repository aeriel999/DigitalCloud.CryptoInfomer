using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels
{
    public partial class ConverterViewModel : ObservableObject
    {
        private readonly ICoinGeckoClient _coinGeckoClient;

        public ConverterViewModel(ICoinGeckoClient coinGeckoClient)
        {
            _coinGeckoClient = coinGeckoClient;
            LoadAsyncCommand = new AsyncRelayCommand(LoadAsync);
            _ = LoadAsync();
        }

        public sealed record DropdownCoin(string Display, string Value);

        [ObservableProperty] private ObservableCollection<DropdownCoin> _fromCurrencies = new();
        public ICollectionView FromCurrenciesView { get; private set; } = null!;

        [ObservableProperty] private string? _selectedFromId;

        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (SetProperty(ref _filterText, value))
                    FromCurrenciesView?.Refresh();
            }
        }

        public IAsyncRelayCommand LoadAsyncCommand { get; }

        private async Task LoadAsync()
        {
            var res = await _coinGeckoClient.GetCoinsListForDropdawnAsync();
            if (res.IsError) return;

            var comparer = StringComparer.Create(CultureInfo.CurrentCulture, ignoreCase: true);

            var list = res.Value
                .Select(x => new DropdownCoin($"{x.Name} ({x.Symbol.ToUpperInvariant()})", x.Id))
                .OrderBy(x => char.IsLetter(x.Display.FirstOrDefault()) ? 0 : 1)
                .ThenBy(x => x.Display, comparer)
                .ToList();

            FromCurrencies = new ObservableCollection<DropdownCoin>(list);

            FromCurrenciesView = CollectionViewSource.GetDefaultView(FromCurrencies);
            FromCurrenciesView.Filter = o =>
            {
                if (string.IsNullOrWhiteSpace(FilterText)) return true;
                var s = FilterText.Trim();
                var item = (DropdownCoin)o;
                return item.Display.Contains(s, StringComparison.CurrentCultureIgnoreCase);
            };

            SelectedFromId ??= FromCurrencies.FirstOrDefault()?.Value;
        }
    }
}

