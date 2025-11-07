using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces;
using DigitalCloud.CryptoInfomer.UI.Views.Pages;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Search;
using DigitalCloud.CryptoInformer.Application.Models.Response.Search;
using System.Collections.ObjectModel;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    private readonly ICoinGeckoClient _client;

    private readonly IDigitalCloudNavigationService _navigation;


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SearchCommand))] 
    private string _query;
    public ObservableCollection<CoinSearchItem> Items { get; } = new();


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SearchCommand))] 
    private bool _isBusy;


    private bool CanSearch() => !IsBusy && !string.IsNullOrWhiteSpace(Query);


    public SearchViewModel(ICoinGeckoClient client, IDigitalCloudNavigationService navigation)
    {
        _client = client;
        _navigation = navigation;

        _query = string.Empty;
    }


    [RelayCommand]
    private void OpenCoinDetails(string id)   
    {
        if (string.IsNullOrWhiteSpace(id)) return;
        _navigation.NavigateTo<CoinDetailsPage, string>(id);
    }


    [RelayCommand(CanExecute = nameof(CanSearch))]  
    private async Task Search()  
    {
        try
        {
            IsBusy = true;  
            Items.Clear();

            var searchingResultOrError = await _client.GetDataForSearchAsync(
                new GetSearchCoinsRequest(Query.Trim()));

            if (searchingResultOrError.IsError)
            {
                // TODO: make error visualization
                return;
            }

            foreach (var c in searchingResultOrError.Value.Coins)
                Items.Add(c);
        }
        finally
        {
            IsBusy = false;  
        }
    }


    public void Reset()
    {
        IsBusy = false;
        Query = string.Empty;
        Items.Clear();
    }
}

