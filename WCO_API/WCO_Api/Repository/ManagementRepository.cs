using WCO_Api.Models;
using WCO_Api.WEBModels;

namespace WCO_Api.Data
{
    public class ManagementRepository
    {

        SQLDB sQLDB = new SQLDB();

        public async Task<int> createNewTournament(TournamentWEB tournament)
        {
            return await sQLDB.insertTournament(tournament);
        }

        public async Task<IEnumerable<Tournament>> getTournaments()
        {
            return await sQLDB.getTournaments();
        }


    }
}
