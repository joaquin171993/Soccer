using Prism.Navigation;
using Soccer.Common.Models;
using Soccer.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Soccer.Prism.ViewModels
{
    public class TournamentPageViewModel : ViewModelBase
    {
        private readonly IApiService apiService;

        public ObservableCollection<TournamentResponse> Tournaments { get; set; }

        public TournamentPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            this.apiService = apiService; 

            Title = "Soccer";

            LoadTournaments();
        }

        private async void LoadTournaments()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            var response = await apiService.GetListAsync<TournamentResponse>(url, "/api", "/Tournaments");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Atención", "Ha ocurrido un error", "Ok");
                return;
            }

            Tournaments = new ObservableCollection<TournamentResponse>((List<TournamentResponse>)response.Result);

        }

    }
}
