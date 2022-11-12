using Microsoft.Data.SqlClient;
using WCO_Api.Models;
using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    public class MatchDatabase
    {

        string CONNECTION_STRING;

        public MatchDatabase()
        {
            CONNECTION_STRING = "Data Source=localhost;Initial Catalog=WCODB;Integrated Security=True";
        }

        /*
         * Query para insertar un partido
         */

        public async Task<int> insertMatch(MatchWEB match)
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            //Hace el insert a la tabla de partidos

            string query =
                          $"INSERT INTO [dbo].[Match] ([startTime], [date], [venue], [scoreT1], [scoreT2] , [bracket_id])" +
                          $"VALUES ('{match.startTime}', '{match.date}', '{match.venue}', '{match.scoreT1}', '{match.scoreT2}' , '{match.bracketId}');";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            string query2 = "SELECT TOP 1 * FROM [dbo].[Match] ORDER BY [m_id] DESC;";

            SqlCommand sqlCmd2 = new SqlCommand(query2, myConnection);

            myConnection.Open();

            var created2 = sqlCmd2.ExecuteNonQuery();
            reader = sqlCmd2.ExecuteReader();

            int MId = 0;

            while (reader.Read()) {
                Console.WriteLine("EL ID ES");
                Console.WriteLine(reader.GetValue(0));
                MId = (int)reader.GetValue(0);
            }

            myConnection.Close();

            //Relacionar talba Team_Match

            string query3 =
                          $"INSERT INTO [dbo].[Match_Team] ([team_id], [match_id])" +
                          $"VALUES ('{match.idTeam1}', '{MId}');" +
                          $"INSERT INTO [dbo].[Match_Team] ([team_id], [match_id])" +
                          $"VALUES ('{match.idTeam2}', '{MId}');";

            SqlCommand sqlCmd3 = new SqlCommand(query3, myConnection);

            myConnection.Open();

            var created3 = sqlCmd3.ExecuteNonQuery();
            myConnection.Close();


            return created;

        }

        /*
         * Query para obtener una lista de MatchOut respecto a un id de bracket
         */

        public async Task<List<MatchOut>> getMatchesByBracketId(int id)
        {
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Match]" +
                $"WHERE bracket_id = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<MatchOut> matches = new List<MatchOut>();

            while (reader.Read())
            {
                MatchOut newMatch = new MatchOut();

                //Obtener info del partido en si

                newMatch.MId = (int)reader.GetValue(0);
                newMatch.startTime = reader.GetValue(1).ToString();
                newMatch.date = reader.GetValue(2).ToString();
                newMatch.venue = reader.GetValue(3).ToString();
                newMatch.bracketId = (int)reader.GetValue(4);

                matches.Add(newMatch);

            }

            myConnection.Close();

            return matches;
        }

        /*
         * Query para obtener una lista de MatchOut respecto a su match_id
         */

        public async Task<List<MatchOut>> getMatchById(int id)
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Match]" +
                $"WHERE m_id = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<MatchOut> matchL = new List<MatchOut>();
            List<TeamWEB> teams = getTeamsByMatchId(id).Result;

            while (reader.Read())
            {
                MatchOut match = new MatchOut();

                match.MId = (int)reader.GetValue(0);
                match.startTime = reader.GetValue(1).ToString();
                match.date = reader.GetValue(2).ToString();
                match.venue = reader.GetValue(3).ToString();
                match.scoreT1 = (int)reader.GetValue(4);
                match.scoreT2 = (int)reader.GetValue(5);
                match.bracketId = (int)reader.GetValue(6);
                match.teams = teams;

                matchL.Add(match);

            }

            return matchL;
        }

        public async Task<List<TeamWEB>> getTeamsByMatchId(int id)
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT [te_id], [name] " +
                $"FROM [dbo].[Match_Team]" +
                $"INNER JOIN[dbo].[Team]" +
                $"ON[dbo].[Team].[te_id] = [dbo].[Match_Team].[team_id] AND[dbo].[Match_Team].match_id = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<TeamWEB> teams = new List<TeamWEB>();

            while (reader.Read())
            {
                TeamWEB team = new TeamWEB();

                team.TeId = (int)reader.GetValue(0);
                team.Name = reader.GetValue(1).ToString();

                teams.Add(team);

            }

            return teams;
        }

        public async Task<List<PlayerWEB>> getPlayersbyTeamId(int id)
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Player]" +
                $"WHERE team_id = '{id}'";
                

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<PlayerWEB> players = new List<PlayerWEB>();

            while (reader.Read())
            {
                PlayerWEB player = new PlayerWEB();

                player.PId = (int)reader.GetValue(0);
                player.name = reader.GetValue(1).ToString();
                player.TId = (int)reader.GetValue(2);

                players.Add(player);

            }

            return players;
        }

        public async Task<List<PlayerWEB>> getPlayersbyBothTeamId(int id1, int id2)
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Player]" +
                $"WHERE team_id = '{id1}' or team_id = '{id2}'";


            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<PlayerWEB> players = new List<PlayerWEB>();

            while (reader.Read())
            {
                PlayerWEB player = new PlayerWEB();

                player.PId = (int)reader.GetValue(0);
                player.name = reader.GetValue(1).ToString();
                player.TId = (int)reader.GetValue(2);

                players.Add(player);

            }

            return players;
        }

    }
}
