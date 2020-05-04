using Soccer.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess.Data.Inicializador
{
    public class SeedDb
    {

        private readonly ApplicationDbContext dbContext;

        public SeedDb(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            await dbContext.Database.EnsureCreatedAsync();
            await CheckTeamsAsync();
            await CheckTournamentsAsync();

        }

        private async Task CheckTournamentsAsync()
        {
            if (!dbContext.Tournaments.Any())
            {
                DateTime startDate = DateTime.Today.AddMonths(2).ToUniversalTime();
                DateTime endDate = DateTime.Today.AddMonths(3).ToUniversalTime();

                dbContext.Tournaments.Add(new TournamentEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    LogoPath = $"~/images/Tournaments/Copa America 2020.png",
                    Name = "Copa America 2020",
                    Groups = new List<GroupEntity>
                    {
                        new GroupEntity
                        {
                             Name = "A",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Panama") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Canada") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Panama"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Canada")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Panama")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Canada")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Canada"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Panama")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "B",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Mexico") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Mexico"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Mexico")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Mexico")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "C",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "USA") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "USA"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "USA")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(11).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(11).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "USA")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "D",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Costa Rica") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Honduras") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Costa Rica"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Honduras")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Costa Rica")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Honduras")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Honduras"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Costa Rica")
                                 }
                             }
                        }
                    }
                });

                startDate = DateTime.Today.AddMonths(1).ToUniversalTime();
                endDate = DateTime.Today.AddMonths(4).ToUniversalTime();

                dbContext.Tournaments.Add(new TournamentEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    LogoPath = $"~/images/Tournaments/Liga Aguila 2020-I.png",
                    Name = "Liga Aguila 2020-I",
                    Groups = new List<GroupEntity>
                    {
                        new GroupEntity
                        {
                             Name = "A",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "America") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Bucaramanga") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Junior") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Medellin") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "America"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Bucaramanga")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Junior"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Medellin")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "America"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Junior")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Bucaramanga"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Medellin")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Medellin"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "America")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Bucaramanga"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Junior")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Bucaramanga"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "America")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Medellin"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Junior")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Junior"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "America")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Medellin"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Bucaramanga")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "America"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Medellin")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Junior"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Bucaramanga")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "B",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Millonarios") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Nacional") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Once Caldas") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Santa Fe") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Millonarios"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Nacional")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Once Caldas"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Santa Fe")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Millonarios"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Once Caldas")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Nacional"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Santa Fe")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Santa Fe"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Millonarios")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Nacional"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Once Caldas")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(16).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Nacional"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Millonarios")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(16).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Santa Fe"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Once Caldas")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(20).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Once Caldas"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Millonarios")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(20).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Santa Fe"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Nacional")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(35).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Millonarios"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Santa Fe")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(35).AddHours(16),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Once Caldas"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Nacional")
                                 }
                             }
                        }
                    }
                });

                startDate = DateTime.Today.AddMonths(1).ToUniversalTime();
                endDate = DateTime.Today.AddMonths(2).ToUniversalTime();

                dbContext.Tournaments.Add(new TournamentEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    LogoPath = $"~/images/Tournaments/Champions.jpg",
                    Name = "Champions 2020",
                    Groups = new List<GroupEntity>
                    {
                        new GroupEntity
                        {
                             Name = "A",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Ajax") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Barcelona") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Ajax"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Barcelona")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Barcelona"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Ajax")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "B",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Bayer Leverkusen") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Chelsea") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Bayer Leverkusen"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Chelsea")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Chelsea"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Bayer Leverkusen")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "C",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Borussia Dortmund") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Inter Milan") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Borussia Dortmund"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Inter Milan")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Inter Milan"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Borussia Dortmund")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "D",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "PSG") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Real Madrid") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "PSG"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Real Madrid")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Real Madrid"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "PSG")
                                 }
                             }
                        }
                    }
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private async Task CheckTeamsAsync()
        {
            if (!dbContext.Teams.Any())
            {
                await AddTeamAsync("Ajax");
                await AddTeamAsync("America");
                await AddTeamAsync("Argentina");
                await AddTeamAsync("Barcelona");
                await AddTeamAsync("Bayer Leverkusen");
                await AddTeamAsync("Bolivia");
                await AddTeamAsync("Borussia Dortmund");
                await AddTeamAsync("Brasil");
                await AddTeamAsync("Bucaramanga");
                await AddTeamAsync("Canada");
                await AddTeamAsync("Chelsea");
                await AddTeamAsync("Chile");
                await AddTeamAsync("Colombia");
                await AddTeamAsync("Costa Rica");
                await AddTeamAsync("Ecuador");
                await AddTeamAsync("Honduras");
                await AddTeamAsync("Inter Milan");
                await AddTeamAsync("Junior");
                await AddTeamAsync("Juventus");
                await AddTeamAsync("Liverpool");
                await AddTeamAsync("Medellin");
                await AddTeamAsync("Mexico");
                await AddTeamAsync("Millonarios");
                await AddTeamAsync("Nacional");
                await AddTeamAsync("Once Caldas");
                await AddTeamAsync("Panama");
                await AddTeamAsync("Paraguay");
                await AddTeamAsync("Peru");
                await AddTeamAsync("PSG");
                await AddTeamAsync("Real Madrid");
                await AddTeamAsync("Santa Fe");
                await AddTeamAsync("Uruguay");
                await AddTeamAsync("USA");
                await AddTeamAsync("Venezuela");
                await dbContext.SaveChangesAsync();
            }
        }

        private async Task AddTeamAsync(string name)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\Teams", $"{name}.jpg");
            await dbContext.Teams.AddAsync(new TeamEntity { Name = name, LogoPath = path });
        }
    }
}
