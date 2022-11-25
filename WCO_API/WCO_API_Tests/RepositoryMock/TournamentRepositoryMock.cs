using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCO_Api.Repository;
using WCO_Api.WEBModels;
namespace WCO_API_Tests.RepositoryMock
{
    public class TournamentRepositoryMock : ITournamentRepository
    {
        public Task<int> createNewTournament(TournamentWEB tournament)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BracketWEB>> getBracketsByTournamentId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeamWEB>> getTeamByTournamentId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeamWEB>> getTeamsByType(string type)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TournamentOut>> getTournaments()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TournamentOut>> getTournamentsById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
