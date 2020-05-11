namespace Soccer.Common.Models
{
    public class TeamResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

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
