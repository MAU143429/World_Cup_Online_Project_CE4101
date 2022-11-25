using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    public interface IPredictionDatabase
    {
        Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch);
        Task<int> insertPrediction(PredictionWEB prediction);
        Task<int> insertPredictionPlayer(PredictionPlayerWEB predPlayer);
    }
}