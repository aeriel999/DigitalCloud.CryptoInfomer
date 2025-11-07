using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Search;
using DigitalCloud.CryptoInformer.Application.Models.Response.Search;
using System.Collections.ObjectModel;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    private readonly ICoinGeckoClient _client;

    [ObservableProperty] private string _query = string.Empty;
    public ObservableCollection<CoinSearchItem> Items { get; } = new();

    public IRelayCommand SearchCommand { get; }

    public SearchViewModel(ICoinGeckoClient client)
    {
        _client = client;
        SearchCommand = new AsyncRelayCommand(DoSearchAsync, () => !string.IsNullOrWhiteSpace(Query));
        PropertyChanged += (_, e) => { if (e.PropertyName == nameof(Query)) (SearchCommand as AsyncRelayCommand)!.NotifyCanExecuteChanged(); };
    }

    private async Task DoSearchAsync()
    {
        Items.Clear();

        var searchResultOrError = await _client.GetDataForSearchAsync(
            new GetSearchCoinsRequest(Query.Trim()));

        if (searchResultOrError.IsError)
        {
            //TODO make error visualization
            return;
        }

        foreach (var c in searchResultOrError.Value.Coins) Items.Add(c);
    }
}

