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

        /*
         * 
         *         
        public string startTime { get; set; }

        public string date { get; set; }

        public string venue { get; set; }

        public int? scoreT1 { get; set; } = 0;

        public int? scoreT2 { get; set; } = 0;

        public int bracketId { get; set; }

        public int idTeam1 { get; set; }

        public int idTeam2 { get; set; }
         * **/

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

    }


}
