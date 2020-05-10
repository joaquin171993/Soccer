using Microsoft.EntityFrameworkCore;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;
using Soccer.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess.Data.Inicializador
{
    public class SeedDb
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly Random _random;

        public SeedDb(ApplicationDbContext dbContext, IContenedorTrabajo contenedorTrabajo)
        {
            this.dbContext = dbContext;
            this.contenedorTrabajo = contenedorTrabajo;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await dbContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckTeamsAsync();
            await CheckTournamentsAsync();
            await CheckUserAsync("207080606", "Joaquín", "Salas", "joasmol.17@gmail.com", "+506 61091434", "San Ramón", UserType.Admin, "");
            await CheckUserAsync("207080606", "Joaquín", "Salas", "joaquinsalas-1993@hotmail.com", "+506 61091434", "San Ramón", UserType.Admin, "");
            await CheckUserAsync("207080606", "Joaquín", "Salas", "josejoaquin.salas@uned.cr", "+506 61091434", "San Ramón", UserType.User, "");
            await CheckUserAsync("207080606", "Joaquín", "Salas", "jsalasm@naglo-cr.com", "+506 61091434", "San Ramón", UserType.User, "");
            await CheckPreditionsAsync();
        }

        private async Task CheckPreditionsAsync()
        {
            if (!dbContext.Predictions.Any())
            {
                foreach (ApplicationUser user in dbContext.Users)
                {
                    if (user.UserType == UserType.User)
                    {
                        AddPrediction(user);
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }

        private void AddPrediction(ApplicationUser user)
        {
            foreach (MatchEntity match in dbContext.Matches)
            {
                dbContext.Predictions.Add(new PredictionEntity
                {
                    GoalsLocal = _random.Next(0, 5),
                    GoalsVisitor = _random.Next(0, 5),
                    Match = match,
                    User = user
                });
            }
        }

        private async Task<ApplicationUser> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType,
            string image)
        {
            ApplicationUser user = await contenedorTrabajo.Usuario.GetUserByEmailAsync(email);
            if (user == null)
            {

                user = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    Team = await dbContext.Teams.FirstOrDefaultAsync(),
                    UserType = userType
                };

                await contenedorTrabajo.Usuario.AddUserAsync(user, "123456");
                await contenedorTrabajo.Usuario.AddRoleToUserAsync(user, userType.ToString());

                string token = await contenedorTrabajo.Usuario.GenerateEmailConfirmationTokenAsync(user);
                await contenedorTrabajo.Usuario.ConfirmEmailAsync(user, token);
            }

            return user;
        }


        private async Task CheckRolesAsync()
        {
            await contenedorTrabajo.Usuario.CheckRoleAsync(UserType.Admin.ToString());
            await contenedorTrabajo.Usuario.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckTournamentsAsync()
        {
            if (!dbContext.Tournaments.Any())
            {
                DateTime startDate = DateTime.Today.AddMonths(13).AddDays(8).ToUniversalTime(); //12/6/2021
                DateTime endDate = DateTime.Today.AddMonths(14).AddDays(7).ToUniversalTime();  //11-7-2021

                dbContext.Tournaments.Add(new TournamentEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = false,
                    LogoPath = $"~/images/Tournaments/Copa America 2021.png",
                    Name = "Copa America 2021",
                    Groups = new List<GroupEntity>
                    {
                        new GroupEntity
                        {
                             Name = "A",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Australia") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(20),   //12-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile"),
                                     NumberMatch = 1
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(16), //13-6-2021 16:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Australia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     NumberMatch = 2
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(19), //13-6-2021 19:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     NumberMatch = 3
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(19), //16-6-2021 19:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     NumberMatch = 7
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(22), //17-6-2021 22:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     NumberMatch = 8                                  
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(19), //17-6-2021 19:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Australia"),
                                     NumberMatch = 9
                                 },  
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(8).AddHours(20), //20-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     NumberMatch = 13
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(19), //21-6-2021 19:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile"),
                                     NumberMatch = 14
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(19), //22-6-2021 19:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Australia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     NumberMatch = 16
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(13).AddHours(20),//25-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     NumberMatch = 19
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(14).AddHours(19), //26-6-2021 19:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     NumberMatch = 20
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(14).AddHours(22), //26-6-2021 22:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Australia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     NumberMatch = 21
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(20), //30-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Chile"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Australia"),
                                     NumberMatch = 25
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(18).AddHours(20), //30-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     NumberMatch = 26
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(16).AddHours(20), //30-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     NumberMatch = 27
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "B",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Qatar") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador") },
                                 new GroupDetailEntity { Team = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                    Date = startDate.AddDays(1).AddHours(20), //13-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     NumberMatch = 4
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(17), //14-6-2021 17:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     NumberMatch = 5
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(20), //14-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Qatar"),
                                     NumberMatch = 6
                                     
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(20), //17-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     NumberMatch = 10
                                     
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(17), //18-6-2021 17:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Qatar"),
                                     NumberMatch = 11
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(20), //18-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     NumberMatch = 12
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(20), //21-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru"),
                                     NumberMatch = 15
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(20), //22-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     NumberMatch = 17
                                 },
                                 new MatchEntity
                                 {
                                    Date = startDate.AddDays(10).AddHours(18), //22-6-2021 18:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Qatar"),
                                     NumberMatch = 18
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(17), //27-6-2021 17:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru"),
                                     NumberMatch = 22
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(20), //27-6-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     NumberMatch = 23
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(16), //27-6-2021 16:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Qatar"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     NumberMatch = 24
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(20), //1-7-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Qatar"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     NumberMatch = 28
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(20), //1-7-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     NumberMatch = 29
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(20), //1-7-2021 20:00 pm
                                     Local = dbContext.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     Visitor = dbContext.Teams.FirstOrDefault(t => t.Name == "Peru"),
                                     NumberMatch = 30
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
                //Grupo A
                await AddTeamAsync("Argentina");
                await AddTeamAsync("Australia");
                await AddTeamAsync("Bolivia");
                await AddTeamAsync("Uruguay");
                await AddTeamAsync("Chile");
                await AddTeamAsync("Paraguay");

                //Grupo B
                await AddTeamAsync("Colombia");
                await AddTeamAsync("Brasil");
                await AddTeamAsync("Qatar");
                await AddTeamAsync("Venezuela");
                await AddTeamAsync("Ecuador");
                await AddTeamAsync("Peru");

                await dbContext.SaveChangesAsync();
            }
        }

        private async Task AddTeamAsync(string name)
        {
            await dbContext.Teams.AddAsync(new TeamEntity { Name = name, LogoPath = $"~/images/Teams/{name}.jpg" });
        }
    }
}
