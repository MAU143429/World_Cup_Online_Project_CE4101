using WCO_Api.Database;
using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    public class PredictionRepository
    {
        PredictionDatabase sQLDB = new PredictionDatabase();

        public async Task<int> createNewPrediction(PredictionWEB prediction)
        {
            return await sQLDB.insertPrediction(prediction);
        }

        public async Task<int> createPredictionPlayer(PredictionPlayerWEB predPlayer)
        {
            return await sQLDB.insertPredictionPlayer(predPlayer);
        }

        public async Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch)
        {
            return await sQLDB.getPredictionByNEM(nickname, email, idMatch);
        }
    }
}
