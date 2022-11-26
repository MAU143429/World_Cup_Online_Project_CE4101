using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCO_Api.Logic;
using WCO_Api.Controllers;
using WCO_Api.WEBModels;
using Xunit;
namespace WCO_API_Tests.Logic
{
    public class ScoreCalculatorTest
    {
        ScoreCalculator scoreCalculator;


        /// <summary>
        /// Method <c>asistsTestCorrectPrediction</c> método que calcula el puntaje 
        /// de una predicción exacta de asistencias de un usuario con respecto
        /// al resultado ingresado por un administrador de X-FIFA
        /// </summary>
        /// 
        [Fact]
        public void asistsTestCorrectPrediction()
        {
            //Arrange
            this.scoreCalculator = new ScoreCalculator();
            int adminAssistsRegisteredP1 = 2;
            int adminAssistsRegisteredP2 = 1;
            int userAssistsRegisteredP1 = 2;
            int userAssistsRegisteredP2 = 1;
            List<PredictionPlayerWEB> adminResult = GetPredictionPlayersAssists(adminAssistsRegisteredP1, adminAssistsRegisteredP2);
            List<PredictionPlayerWEB> userPrediction = GetPredictionPlayersAssists(userAssistsRegisteredP1, userAssistsRegisteredP2);
            //Act
            var result = this.scoreCalculator.asistsPts(adminResult, userPrediction);
            //Assert
            Assert.Equal(15f, result);
        }

        /// Method <c>asistsTestCorrectPrediction</c> método que calcula el puntaje 
        /// de una predicción exacta de goles de un usuario con respecto
        /// al resultado ingresado por un administrador de X-FIFA
        /// </summary>
        /// 
        [Fact]
        public void goalsTestCorrectPrediction()
        {
            //Arrange
            this.scoreCalculator = new ScoreCalculator();
            int adminGoalsRegisteredP1 = 2;
            int adminGoalsRegisteredP2 = 1;
            int userGoalsRegisteredP1 = 2;
            int userGoalsRegisteredP2 = 1;
            List<PredictionPlayerWEB> adminResult = GetPredictionPlayersGoals(adminGoalsRegisteredP1, adminGoalsRegisteredP2);
            List<PredictionPlayerWEB> userPrediction = GetPredictionPlayersGoals(userGoalsRegisteredP1, userGoalsRegisteredP2);
            //Act
            var result = this.scoreCalculator.goalsPts(adminResult, userPrediction);
            //Assert
            Assert.Equal(15f, result);
        }

        private static List<PredictionPlayerWEB> GetPredictionPlayersAssists(int assistsP1, int assistsP2)
        {
            var predPlayer1 = new PredictionPlayerWEB()
            {
                PId = 1,
                assists = assistsP1,
                goals = 0,
                PrId = 2
            };
            var predPlayer2 = new PredictionPlayerWEB()
            {
                PId = 1,
                assists = assistsP2,
                goals = 0,
                PrId = 2
            };
            List<PredictionPlayerWEB> predictionPlayers = new() { predPlayer1, predPlayer2 };

            return predictionPlayers;
        }

        private static List<PredictionPlayerWEB> GetPredictionPlayersGoals(int goalsP1, int goalsP2)
        {
            var predPlayer1 = new PredictionPlayerWEB()
            {
                PId = 1,
                assists = 0,
                goals = goalsP1,
                PrId = 2
            };
            var predPlayer2 = new PredictionPlayerWEB()
            {
                PId = 1,
                assists = 0,
                goals = goalsP2,
                PrId = 2
            };
            List<PredictionPlayerWEB> predictionPlayers = new() { predPlayer1, predPlayer2 };

            return predictionPlayers;
        }
    }
}
