using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCO_Api.Controllers;
using WCO_Api.WEBModels;
using Xunit;

namespace WCO_API_Tests
{
    public class TournamentControllerTest
    {
        TournamentController tournamentControllerMock;
        readonly TournamentWEB tournamentWEBMock;

        public TournamentControllerTest()
        {
            tournamentControllerMock = new TournamentController();
            tournamentWEBMock = new TournamentWEB()
            {
                Name = "UEFA Champions League",
                StartDate = "11-20-2022",
                EndDate = "12-19-2022",
                brackets = new List<string> { "Semis", "Final" },
                Description = "",
                teams = new List<int>{ 0, 1, 2, 3 },
                Type = "Local"
            };
        }

        [Fact]

        /// <summary>
        /// Method <c>insertTournamentTest</c> método que prueba si se ingresó un nuevo torneo a la base de datos 
        /// de acuerdo con el modelo TournamentWEB establecido en el constructor de forma existosa
        /// </summary>
        public async Task insertTournamentTestAsync()
        {

            //Arrange
            //Act
            var result = await this.tournamentControllerMock.createTournament(this.tournamentWEBMock);
            //Assert
            Assert.IsType<CreatedResult>(result);
        }
        /// <summary>
        /// Method <c>getTournamentById</c> método que prueba una extracción del torneo ingresado según su identificador
        /// de forma exitosa
        /// </summary>
        [Fact]
        public async Task getTournamentByIdTest()
        {
            //Arrange
            string onTestId = "4fc417";
            //Act
            var result = await this.tournamentControllerMock.getTournamentById(onTestId);
            //Assert
            Assert.IsType<List<TournamentOut>>(result);
        }

        /// <summary>
        /// Method <c>getTournamentById</c> método que prueba una extracción de todos los torneos
        /// de forma existosa
        /// </summary>
        [Fact]
        public async Task getTournamentsTest()
        {
            //Arrange
            //Act
            var result = await this.tournamentControllerMock.getTournaments();
            //Assert
            Assert.IsType<List<TournamentOut>>(result);
        }

        /// <summary>
        /// Method <c>getTeamsByType</c> método que prueba una extracción de los tipos de equipo en el torneo
        /// de forma exitosa
        /// </summary>
        [Fact]
        public async Task getTeamsByTypeTest()
        {
            //Arrange
            string tournamentTyoe = "Local";
            //Act
            var result = await this.tournamentControllerMock.getTeamsByType(tournamentTyoe);
            //Assert
            Assert.IsAssignableFrom<IEnumerable<TeamWEB>>(result);
        }

        /// <summary>
        /// Method <c>getTeamsByType</c> método que prueba una extracción de los equipos asociados 
        /// a un torneo creado por medio del ID de forma exitosa
        [Fact]
        public async Task getTeamsByTournamentIdTest()
        {
            //Arrange
            string onTestId = "df0134";
            //Act
            var result = await this.tournamentControllerMock.GetTeamsByTournamentId(onTestId);
            //Assert
            Assert.IsAssignableFrom<IEnumerable<TeamWEB>>(result);
        }
    }
}