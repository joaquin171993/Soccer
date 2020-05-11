using Prism.Commands;
using Prism.Navigation;
using Soccer.Common.Models;
using Soccer.Prism.Views;
using System;

namespace Soccer.Prism.ViewModels
{
    public class TournamentItemViewModel : TournamentResponse
    {
        private readonly INavigationService navigationService;
        private DelegateCommand _selectTournamentCommand;

        public TournamentItemViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public DelegateCommand SelectTournamentCommand => _selectTournamentCommand ?? (_selectTournamentCommand = new DelegateCommand(SelectTournamentAsync));

        private async void SelectTournamentAsync()
        {

            var parameters = new NavigationParameters
            {

                { "tournament", this }

            };

            await navigationService.NavigateAsync(nameof(GroupPage), parameters);
        }

    }
}
