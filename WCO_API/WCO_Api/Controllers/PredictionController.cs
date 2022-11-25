using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        IPredictionRepository predRepository = new PredictionRepository();

        /*
         * Recibe un objeto PredictionWEB, se hacen las operaciones necesarias para poder crear una prediccion
         * Retorna un Task<IActionResult> indicando si se pudo crear la predicción o no
         */

        [HttpPost("AddPrediction")]
        public async Task<IActionResult> createPrediction( PredictionWEB prediction)
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
                var createdP = await predRepository.createNewPrediction(prediction);

                if(createdP == 1)
                {
                    return Created("api/Prediction/AddPrediction", "Se creo una prediccion exitosamente");
                } else
                {
                    return StatusCode(500, "Error al crear una prediccion");
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
        public async Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch )
        {
            return await predRepository.getPredictionByNEM(nickname, email, idMatch);
        }

    }
}
