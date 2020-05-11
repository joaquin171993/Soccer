using Acr.UserDialogs;
using Prism.Navigation;
using Soccer.Common.Models;
using Soccer.Common.Services;
using Soccer.Prism.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class TournamentPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IApiService apiService;

        public List<TournamentItemViewModel> Tournaments { get; set; }

        public TournamentPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.apiService = apiService;

            Title = "Soccer";

            LoadTournaments();
        }

        private async void LoadTournaments()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            string url = App.Current.Resources["UrlAPI"].ToString();
            var response = await apiService.GetListAsync<TournamentResponse>(url, "/api", "/Tournaments");
            UserDialogs.Instance.HideLoading();

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Atención", "Ha ocurrido un error", "Ok");
                return;
            }

            var tournaments = (List<TournamentResponse>)response.Result;

            Tournaments = tournaments.Select(t => new TournamentItemViewModel(navigationService)
            {

                EndDate = t.EndDate,
                Groups = t.Groups,
                Id = t.Id,
                IsActive = t.IsActive,
                LogoPath = t.LogoPath,
                Name = t.Name,
                StartDate = t.StartDate

            }).ToList();

        }

    }
}
