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

        public async Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch)
        {
            return await sQLDB.getPredictionByNEM(nickname, email, idMatch);
        }

        public async Task<List<PredictionWEB>> getPredictionByMatchId(int idMatch)
        {
            return await sQLDB.getPredictionByMatchId(idMatch);
        }

        public async Task<int> setPredictionPoints(int? predId, float points)
        {
            return await sQLDB.setPredictionPoints(predId, points);
        }
        /*
        public async Task<int> setTournamentPoints(PredictionWEB pred, int points)
        {
            return await sQLDB.setTournamentPoints(pred, points);
        }
        */
    }
}
