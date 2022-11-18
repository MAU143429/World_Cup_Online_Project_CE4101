using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Repository;
using WCO_Api.WEBModels;

namespace WCO_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {

        PredictionRepository predRepository = new PredictionRepository();

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

        [HttpGet("getPredictionByNEM/{nickname}/{email}/{idMatch}")]
        public async Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch )
        {
            return await predRepository.getPredictionByNEM(nickname, email, idMatch);
        }

    }
}
