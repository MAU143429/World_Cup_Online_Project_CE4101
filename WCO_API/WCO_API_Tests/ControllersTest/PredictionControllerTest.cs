using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCO_Api.Controllers;
using WCO_Api.Repository;
using WCO_Api.WEBModels;
using Xunit;
using Moq;


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
        public async Task insertPredictionTestSuccess()
        {
            //Arrange

            var mockRepo = new Mock<IPredictionRepository>();
            mockRepo.Setup(repo => repo.getPredictionByNEM(this.predictionWEBMock.acc_nick, 
                this.predictionWEBMock.acc_email, this.predictionWEBMock.match_id)).ReturnsAsync(this.predictionWEBMock);
            mockRepo.Setup(repo => repo.createNewPrediction(this.predictionWEBMock)).ReturnsAsync(correctInsert());
            var controller = new PredictionController(mockRepo.Object);
            //Act
            var result = await controller.createPrediction(this.predictionWEBMock);
            //Assert 
            var createdResult = Assert.IsType<CreatedResult>(result);

            Assert.Equal("Se creo una prediccion exitosamente", createdResult.Value);
        }

        [Fact]
        public async Task insertPredictionTestNotSuccess()
        {
            //Arrange

            var mockRepo = new Mock<IPredictionRepository>();
            mockRepo.Setup(repo => repo.getPredictionByNEM(this.predictionWEBMock.acc_nick,
               this.predictionWEBMock.acc_email, this.predictionWEBMock.match_id)).ReturnsAsync(this.predictionWEBMock);
            mockRepo.Setup(repo => repo.createNewPrediction(this.predictionWEBMock)).ReturnsAsync(incorrectInsert());
            var controller = new PredictionController(mockRepo.Object);
            //Act
            var result = await controller.createPrediction(this.predictionWEBMock);
            //Assert 
            var output = Assert.IsType<ObjectResult>(result);

            Assert.Equal(500, output.StatusCode.Value);
        }



        [Fact]
        public async Task getPredictionByNEMTest()
        {
            //Arrange
            string nicknameMock = "manumora";
            string emailMock = "jj@gmail.com";
            int matchMock = 0;
            var mockRepo = new Mock<IPredictionRepository>();
            mockRepo.Setup(repo => repo.getPredictionByNEM(nicknameMock, emailMock, matchMock)).ReturnsAsync(this.predictionWEBMock);
            var controller = new PredictionController(mockRepo.Object);
            //Act
            var result = await controller.getPredictionByNEM(nicknameMock, emailMock, matchMock);
            //Assert 
            var output = Assert.IsType<PredictionWEB>(result);

        }

        private int correctInsert()
        {
            return 1;
        }

        private int incorrectInsert()
        {
            return -1;
        }
    }
}
