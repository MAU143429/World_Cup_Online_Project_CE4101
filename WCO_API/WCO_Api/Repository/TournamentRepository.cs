using WCO_Api.Database;
using WCO_Api.WEBModels;

/*
 * Repositorio que contiene toda la funcionalidad relacionada con torneos 
*/

namespace WCO_Api.Repository
{
    public class TournamentRepository
    {

        TournamentDatabase sQLDB = new TournamentDatabase();

        public async Task<int> createNewTournament(TournamentWEB tournament)
        {
            return await sQLDB.insertTournament(tournament);
        }

        public async Task<IEnumerable<TournamentOut>> getTournaments()
        {
            return await sQLDB.getTournaments();
        }

        public async Task<IEnumerable<TournamentOut>> getTournamentsById(string id)
        {
            return await sQLDB.getTournamentsById(id);
        }

        public async Task<IEnumerable<TeamWEB>> getTeamByTournamentId(string id)
        {
            return await sQLDB.getTeamByTournamentId(id);
        }

        public async Task<IEnumerable<BracketWEB>> getBracketsByTournamentId(string id)
        {
            return await sQLDB.getBracketsByTournamentId(id);

        }

        public async Task<IEnumerable<TeamWEB>> getTeamsByType(string type)
        {
            return await sQLDB.getTeamsByType(type);
        }

    }
}
