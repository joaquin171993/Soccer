using Prism.Navigation;
using Soccer.Common.Helpers;
using Soccer.Common.Models;
using System.Collections.Generic;

namespace Soccer.Prism.ViewModels
{
    public class GroupPageViewModel : ViewModelBase
    {
        private readonly ITransformHelper transformHelper;
        private TournamentResponse Tournament;

        public GroupPageViewModel(INavigationService navigationService, ITransformHelper transformHelper) : base(navigationService)
        {
            this.transformHelper = transformHelper;
        }

        public List<Group> Groups { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters) /*página que recupera el parámetro que viene de la otra página*/
        {
            base.OnNavigatedTo(parameters);

            Tournament = parameters.GetValue<TournamentResponse>("tournament");

            Title = Tournament.Name;

            Groups = transformHelper.ToGroups(Tournament.Groups);

        }

    }
}
