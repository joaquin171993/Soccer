using System;

namespace Soccer.Common.Models
{
    public class TournamentResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public bool IsActive { get; set; }

        public string LogoPath { get; set; }

        public string LogoFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.LogoPath))
                {
                    return "torneo";
                }

                return $"https://soccerapp.azurewebsites.net{this.LogoPath.Substring(1)}";
            }

        }

    }
}
