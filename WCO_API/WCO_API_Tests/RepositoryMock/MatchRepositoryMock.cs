using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCO_Api.Repository;
using WCO_Api.WEBModels;
namespace WCO_API_Tests.RepositoryMock
{
    public class MatchRepositoryMock : IMatchRepository
    {
        public Task<int> createNewMatch(MatchWEB match)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MatchOut>> getMatchById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MatchOut>> getMatchesByBracketId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayerWEB>> getPlayersbyBothTeamId(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayerWEB>> getPlayersbyTeamId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeamWEB>> getTeamsByMatchId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
