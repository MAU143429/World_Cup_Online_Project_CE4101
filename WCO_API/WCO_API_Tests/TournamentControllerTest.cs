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
        TournamentController _tournamentController;
        readonly TournamentWEB _tournamentWEB;

        public TournamentControllerTest()
        {
            _tournamentController = new TournamentController();
            _tournamentWEB = new TournamentWEB()
            {
                Name = "Australia World Cup",
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
            var result = await this._tournamentController.createTournament(this._tournamentWEB);
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
            //Act
            string onTestId = "4fc417";
            var result = await this._tournamentController.getTournamentById(onTestId);
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
            var result = await this._tournamentController.getTournaments();
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
            //Act
            var result = await this._tournamentController.getTeamsByType("Local");
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
            //Act
            var result = await this._tournamentController.GetTeamsByTournamentId("1d02d8");
            //Assert
            Assert.IsAssignableFrom<IEnumerable<TeamWEB>>(result);
        }
    }
}