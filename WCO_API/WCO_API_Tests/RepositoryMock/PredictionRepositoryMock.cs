using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCO_Api.Repository;
using WCO_Api.WEBModels;
namespace WCO_API_Tests.RepositoryMock
{
    public class PredictionRepositoryMock : IPredictionRepository
    {
        public Task<int> createNewPrediction(PredictionWEB prediction)
        {
            throw new NotImplementedException();
        }

        public Task<int> createPredictionPlayer(PredictionPlayerWEB predPlayer)
        {
            throw new NotImplementedException();
        }

        public Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch)
        {
            throw new NotImplementedException();
        }
    }
}
