using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCO_Api.Controllers;
using WCO_Api.WEBModels;
using Xunit;

/**
 *        public int? PrId { get; set; }

        public int goalsT1 { get; set; }

        public int goalsT2 { get; set; }

        public int? points { get; set; } = 0;

        public int PId { get; set; }

        public string acc_nick { get; set; }

        public string acc_email { get; set; }

        public int match_id { get; set; }

        public List<PredictionPlayerWEB> predictionPlayers { get; set; }
 */
namespace WCO_API_Tests.ControllersTest
{
    public class PredictionControllerTest
    {
        PredictionController predictionControllerMock;
        PredictionWEB predictionWEBMock;
        PredictionPlayerWEB predictionPlayerWEBMock1;
        PredictionPlayerWEB predictionPlayerWEBMock2;
        List<PredictionPlayerWEB> predictionPlayerWEBsMock;
        public PredictionControllerTest()
        {
            predictionControllerMock = new PredictionController();
            predictionPlayerWEBMock1 = new PredictionPlayerWEB() { PId = 4, assists = 0, goals = 3 };
            predictionPlayerWEBMock2 = new PredictionPlayerWEB() { PId = 1, goals = 0, assists = 2 };
            predictionPlayerWEBsMock = new List<PredictionPlayerWEB>()
            { predictionPlayerWEBMock1, predictionPlayerWEBMock2};

            predictionWEBMock = new PredictionWEB()
            {
                acc_email = "jj@gmail.com", acc_nick = "manumora",
                match_id = 0, goalsT1 = 0, goalsT2 = 3, PId = 4,
                predictionPlayers = predictionPlayerWEBsMock
            };
        }

        /// <summary>
        /// Method <c>insertPredictionTest</c> método que hace la inserción de una predicción completa
        /// a la base de datos, es decir, ingresa MVP, marcador, jugadores goleadores y asistentes
        /// </summary>
        [Fact]
        public async Task insertPredictionTest()
        {
            //Arrange
            //Act
            var result = await this.predictionControllerMock.createPrediction(this.predictionWEBMock);
            //Assert 
            Assert.IsType<CreatedResult>(result);
        }

        /// <summary>
        /// Method <c>getPredictionByNEMTest</c> método que hace la consulta de una predicción completa
        /// a la base de datos, mediante llave primaria de usuario y partido. 
        /// </summary>
        [Fact]
        public async Task getPredictionByNEMTest() 
        {
            //Arrange
            string nicknameMock = "manumora";
            string emailMock = "jj@gmail.com";
            int matchMock = 0;
            //Act
            var result = await this.predictionControllerMock.getPredictionByNEM(nicknameMock, emailMock, matchMock);
            //Assert 
            Assert.IsType<PredictionWEB>(result);

        }
    }
}
