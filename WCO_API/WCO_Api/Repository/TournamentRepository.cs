using WCO_Api.Database;
using WCO_Api.Models;
using WCO_Api.WEBModels;

/*
 * Repositorio que contiene toda la funcionalidad relacionada con torneos 
*/

namespace WCO_Api.Repository
{
    public class TournamentRepository
    {

        TournamentDatabase sQLDB = new TournamentDatabase();

        public async Task<int> createNewTournament(Tournament tournament)
        {
            return await sQLDB.insertTournament(tournament);
        }

        public async Task<int> createNewBracket(Bracket bracket)
        {
            return await sQLDB.insertBracket(bracket);
        }

        public async Task<int> updateTeamId(Team team)
        {
            return await sQLDB.updateTeam(team);
        }

        public async Task<IEnumerable<Tournament>> getTournaments()
        {
            return await sQLDB.getTournaments();
        }

        public async Task<IEnumerable<Tournament>> getTournamentsById(string id)
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

        public async Task<int> getTotalBrackets()
        {
            return await sQLDB.getTotalBrackets();
        }

    }
}
