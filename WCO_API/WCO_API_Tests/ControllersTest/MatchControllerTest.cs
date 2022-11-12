using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCO_Api.Controllers;
using WCO_Api.Models;
using WCO_Api.WEBModels;
using Xunit;
namespace WCO_API_Tests.ControllersTest
{
    public class MatchControllerTest
    {
        MatchController matchController;
        MatchWEB matchWEB;


        public MatchControllerTest()
        {
            matchController = new MatchController();
            matchWEB = new MatchWEB() 
            {   
                startTime = "12:00", date = "11-12-2022", 
                idTeam1 = 0, idTeam2 = 1, scoreT1 = 0 , scoreT2 = 0,
                bracketId =  4, venue = "Lisboa" 
            };
        }



        /// <summary>
        /// Method <c>AddMatchTest</c> método que prueba si se ingresó un nuevo partido a la base de datos 
        /// de acuerdo con el modelo MatchWEB establecido en el constructor, de forma existosa
        /// </summary>
        [Fact]
        public async Task AddMatchTest()
        {
            //Arrange
            //Act
            var result = await this.matchController.createMatch(this.matchWEB);
            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        /// <summary>
        /// Method <c>getMatchByIdTest</c> método que prueba la extracción del partido desde la base de datos
        /// de acuerdo con su llave primaria, de forma existosa
        /// </summary>
        [Fact]
        public async Task getMatchByIdTest()
        {
            //Arrange
            int firstMatchCreated = 0;
            //Act
            var result = await this.matchController.getMatchById(firstMatchCreated);
            //Assert
            Assert.IsType<List<MatchOut>>(result);
        }

        /// <summary>
        /// Method <c>getMatchesByTournamentIdTest</c> método que prueba la extracción de los partidos
        /// de un torneo desde la base de datos según la llave primaria, de forma existosa
        /// </summary>
        [Fact]
        public async Task getMatchesByTournamentIdTest()
        {
            //Arrange
            string currentTournamentId = "df0134";
            //Act
            var result = await this.matchController.getMatchesByTournamentId(currentTournamentId);
            //Assert
            Assert.IsType<List<List<MatchOut>>>(result);
        }

        /// <summary>
        /// Method <c>GetPlayersbyTeamIdTest</c> método que prueba la extracción de los jugadores
        /// de un equipo desde la base de datos según la llave primaria, de forma existosa
        /// </summary>

        [Fact]
        public async Task GetPlayersbyTeamIdTest()
        {
            //Arrange
            int id_team1 = 0;
            //Act
            var result = await this.matchController.GetPlayersbyTeamId(id_team1);
            //Assert
            Assert.IsType<List<PlayerWEB>>(result);
        }

        /// <summary>
        /// Method <c>GetPlayersbyBothTeamIdTest</c> método que prueba la extracción de los jugadores
        /// de los dos equipos desde la base de datos según la llave primaria, de forma existosa
        /// </summary>
        [Fact]
        public async Task GetPlayersbyBothTeamIdTest()
        {
            //Arrange
            int id_team1 = 0;
            int id_team2 = 1;
            //Act
            var result = await this.matchController.GetPlayersbyBothTeamId(id_team1, id_team2);
            //Assert
            Assert.IsType<List<PlayerWEB>>(result);
        }
    }


}
