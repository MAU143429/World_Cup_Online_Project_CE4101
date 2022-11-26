using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCO_Api.Controllers;
using WCO_Api.Models;
using WCO_Api.Repository;
using WCO_Api.WEBModels;

using Xunit;
using Moq;

namespace WCO_API_Tests.ControllersTest
{
    public class MatchControllerTest
    {
        MatchController matchControllerMock;
        MatchWEB matchWEBMock;


        public MatchControllerTest()
        {
       
            matchWEBMock = new MatchWEB() 
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
        public async Task AddMatchTestSuccess()
        {
            //Arrange

            var mockRepo = new Mock<IMatchRepository>();
            var mockRepoT = new Mock<ITournamentRepository>();
            mockRepo.Setup(repo => repo.createNewMatch(this.matchWEBMock)).ReturnsAsync(correctInsert());
            var controller = new MatchController(mockRepo.Object, mockRepoT.Object);
            //Act
            var result = await controller.createMatch(this.matchWEBMock);
            //Assert
            var createdResult = Assert.IsType<CreatedResult>(result);

            Assert.Equal(1, createdResult.Value);

        }

        /// <summary>
        /// Method <c>AddMatchTest</c> método que prueba si se ingresó un nuevo partido a la base de datos 
        /// de acuerdo con el modelo MatchWEB establecido en el constructor, de forma fallida
        /// </summary>

        [Fact]
        public async Task AddMatchTestNotSuccess()
        {
            //Arrange

            var mockRepo = new Mock<IMatchRepository>();
            var mockRepoT = new Mock<ITournamentRepository>();
            mockRepo.Setup(repo => repo.createNewMatch(this.matchWEBMock)).ReturnsAsync(incorrectInsert());
            var controller = new MatchController(mockRepo.Object, mockRepoT.Object);
            //Act
            var result = await controller.createMatch(this.matchWEBMock);
            //Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            
            Assert.Equal(-1, createdResult.Value);

        }
        [Fact]
        public async Task AddMatchWithBadRequest()
        {

            //Arrange

            var mockRepo = new Mock<IMatchRepository>();
            var mockRepoT = new Mock<ITournamentRepository>();
            var controller = new MatchController(mockRepo.Object, mockRepoT.Object);
            controller.ModelState.AddModelError("Input", "Null entry detected");
            //Act
            var result = await controller.createMatch(null);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

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
            var mockRepo = new Mock<IMatchRepository>();
            var mockRepoT = new Mock<ITournamentRepository>();
            mockRepo.Setup(repo => repo.getMatchById(firstMatchCreated)).ReturnsAsync(getMatcheslist());
            var controller = new MatchController(mockRepo.Object, mockRepoT.Object);
            //Act
            var result = await controller.getMatchById(firstMatchCreated);
            //Assert
            Assert.IsType<List<MatchOut>>(result);
        }


        /// <summary>
        /// Method <c>GetPlayersbyTeamIdTest</c> método que prueba la extracción de los jugadores
        /// de un equipo desde la base de datos según la llave primaria, de forma existosa
        /// </summary>

        [Fact]
        public async Task GetPlayersbyTeamIdTest()
        {
            //Arrange
            int id_team1 = 1;

            var mockRepo = new Mock<IMatchRepository>();
            var mockRepoT = new Mock<ITournamentRepository>();
            mockRepo.Setup(repo => repo.getPlayersbyTeamId(id_team1)).ReturnsAsync(GetPlayers());
            var controller = new MatchController(mockRepo.Object, mockRepoT.Object);
            //Act
            var result = await controller.GetPlayersbyTeamId(id_team1);
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
            var mockRepo = new Mock<IMatchRepository>();
            var mockRepoT = new Mock<ITournamentRepository>();
            mockRepo.Setup(repo => repo.getPlayersbyBothTeamId(id_team1, id_team2)).ReturnsAsync(GetPlayers());
            var controller = new MatchController(mockRepo.Object, mockRepoT.Object);
            //Act
            var result = await controller.GetPlayersbyBothTeamId(id_team1, id_team2);
            //Assert
            Assert.IsType<List<PlayerWEB>>(result);
        }

        private int correctInsert()
        {
            return 1;
        }

        private int incorrectInsert()
        {
            return -1;
        }  
        private static List<MatchOut> getMatcheslist()
        {
            var team1 = new TeamWEB() { Name = "Real Madrid", TeId = 1 };
            var team2 = new TeamWEB() { Name = "Betis", TeId = 2 };
            var _teams = new List<TeamWEB>();
            _teams.Add(team1); _teams.Add(team2);

            var matchOut = new MatchOut()
            {
                bracketId = 0,
                date = "11-12-2022",
                scoreT1 = 0,
                scoreT2 = 0,
                venue = "Lisboa",
                teams = _teams,
                MId = 1,
                startTime = "19:00"
            };
            var matchOuts = new List<MatchOut>();
            return matchOuts;
        }

        private static List<PlayerWEB> GetPlayers()
        {
            var players = new List<PlayerWEB>();
            var player1 = new PlayerWEB()
            {
                name = "Marcelo",
                PId = 1,
                TId = 1
            };
            var player2 = new PlayerWEB()
            {
                name = "William",
                PId = 2,
                TId = 1
            };
            players.Add(player1); players.Add(player2);
            return players;
        }
    }


}
