using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    public interface ITournamentDatabase
    {
        Task<List<BracketWEB>> getBracketsByTournamentId(string id);
        Task<List<TeamWEB>> getTeamByTournamentId(string id);
        Task<List<TeamWEB>> getTeamsByType(string type);
        Task<List<TournamentOut>> getTournaments();
        Task<List<TournamentOut>> getTournamentsById(string id);
        Task<int> insertTournament(TournamentWEB newTournament);
    }
}