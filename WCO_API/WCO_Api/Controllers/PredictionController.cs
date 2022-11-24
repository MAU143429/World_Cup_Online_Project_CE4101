using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Logic;
using WCO_Api.Repository;
using WCO_Api.WEBModels;

/**
 * Controlador encargado de realizar las acciones relacionadas con las Predicciones
 * 
*/

namespace WCO_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {

        PredictionRepository predRepository = new PredictionRepository();
        ScoreCalculator sc = new();
        /*
         * Recibe un objeto PredictionWEB, se hacen las operaciones necesarias para poder crear una prediccion
         * Retorna un Task<IActionResult> indicando si se pudo crear la predicción o no
         */

        [HttpPost("AddPrediction")]
        public async Task<IActionResult> createPrediction(PredictionWEB prediction)
        {

            //Validaciones de modelo enviado
            if (prediction == null)
                return BadRequest("null input");

            if (prediction.predictionPlayers == null)
                return BadRequest("null prediction players input");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Revisar si la predicción ya existe
            var dbPrediction = predRepository.getPredictionByNEM(prediction.acc_nick, prediction.acc_email, prediction.match_id);

            //Si no existe, la crea
            if (dbPrediction.Result.PrId == null)
            {

                if (prediction.isAdmin)
                {

                    List<PredictionWEB> predictions = predRepository.getPredictionByMatchId(prediction.match_id).Result;

                    foreach (var predictionDB in predictions)
                    {
                        //calcular los puntos para la prediccion
                        float points = sc.calculatePts(predictionDB, prediction);

                        Console.WriteLine("Obtuve " + points + " de prediccion " + predictionDB.PrId);
                        //Setea los puntos de la predicción
                        int result = predRepository.setPredictionPoints(predictionDB.PrId, points).Result;

                    }

                    return StatusCode(500, "Admin");
                }
                else
                {
                    var createdP = await predRepository.createNewPrediction(prediction);

                    if (createdP == 1)
                    {
                        return Created("api/Prediction/AddPrediction", "Se creo una prediccion exitosamente");
                    }
                    else
                    {
                        return StatusCode(500, "Error al crear una prediccion");
                    }
                }

                

            }
            // Si existe, hace más bien un cambio a esa predicción
            else {
                return BadRequest("prediction already made");
            }

        }

        /*
         * Recibe un objeto MarchWEB, se hacen las operaciones necesarias para poder crear un partido
         * Retorna un Task<IActionResult> indicando si se pudo crear el partido o no
         */

        [HttpGet("getPredictionByNEM/{nickname}/{email}/{idMatch}")]
        public async Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch)
        {
            return await predRepository.getPredictionByNEM(nickname, email, idMatch);
        }

        [HttpGet("getPredictionByMatchId/{idMatch}")]
        public async Task<List<PredictionWEB>> getPredictionByMatchId(int idMatch)
        {
            return await predRepository.getPredictionByMatchId(idMatch);
        }

        [HttpPost("comparePrediction")]
        public async Task<double> comparePrediction(PredictionWEB predictionUser)
        {

            PredictionWEB predictionAdmin = new();

            predictionAdmin.TId = "12345678901234";
            predictionAdmin.goalsT1 = 3;
            predictionAdmin.goalsT2 = 0;
            predictionAdmin.winner = 0;
            predictionAdmin.PId = 12;

            List<PredictionPlayerWEB> adminPredPlayers = new();
            
            PredictionPlayerWEB predP1 = new PredictionPlayerWEB
            {
                PId = 12,
                goals = 3,
                assists = 0
            };
            /*
            PredictionPlayerWEB predP2 = new PredictionPlayerWEB
            {
                PId = 11,
                goals = 0,
                assists = 1
            };

            PredictionPlayerWEB predP3 = new PredictionPlayerWEB
            {
                PId = 10,
                goals = 0,
                assists = 1
            };
            */
            adminPredPlayers.Add(predP1);
            //adminPredPlayers.Add(predP2);
            //adminPredPlayers.Add(predP3);
            
            predictionAdmin.predictionPlayers = adminPredPlayers;

            return sc.calculatePts(predictionAdmin, predictionUser);
        }
    }
}
