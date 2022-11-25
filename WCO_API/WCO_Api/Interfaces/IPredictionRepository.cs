using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    public interface IPredictionRepository
    {
        Task<int> createNewPrediction(PredictionWEB prediction);
        Task<int> createPredictionPlayer(PredictionPlayerWEB predPlayer);
        Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch);
    }
}