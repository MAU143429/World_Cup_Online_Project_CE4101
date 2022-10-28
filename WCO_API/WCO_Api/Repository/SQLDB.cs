using Microsoft.AspNetCore.Mvc;
using WCO_Api.Models;
using WCO_Api.WEBModels;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WCO_Api.Data
{
    public class SQLDB : ControllerBase
    {
        String CONNECTION_STRING;
        
        public SQLDB()
        {
            CONNECTION_STRING = "Data Source=localhost;Initial Catalog=WCODB;Integrated Security=True";
        }

        public async Task<int> insertTournament(Tournament newTournament)
        {
            
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"INSERT INTO [dbo].[Tournament] ([to_id], [name], [startDate], [endDate], [description], [type])" +
                          $"VALUES ('{newTournament.ToId}', '{newTournament.Name}', '{newTournament.StartDate}', '{newTournament.EndDate}', '{newTournament.Description}' , '{newTournament.Type}');";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);
            
            myConnection.Open();
            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;

        }

        public async Task<int> insertBracket(Bracket newBracket)
        {

            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = 
                          $"INSERT INTO [dbo].[Bracket] ([b_id], [name], [tournamentId])" +
                          $"VALUES ('{newBracket.BId}', '{newBracket.Name}', '{newBracket.TournamentId}');";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();
            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;

        }

        public async Task<int> insertMatch(MatchWEB match)
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            //Hace el insert a la tabla de partidos

            string query =
                          $"INSERT INTO [dbo].[Match] ([m_id], [startTime], [date], [venue], [bracket_id])" +
                          $"VALUES ('{match.MId}', '{match.startTime}', '{match.date}', '{match.venue}', '{match.bracketId}');";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
            
            string query2 = "SELECT COUNT(*)"+
                            "FROM Match_Team;";

            SqlCommand sqlCmd2 = new SqlCommand(query2, myConnection);

            myConnection.Open();

            var created2 = sqlCmd2.ExecuteNonQuery();
            reader = sqlCmd2.ExecuteReader();

            int x = 0;

            while (reader.Read())
            {
                Console.WriteLine("COUNT DE Match_Team");
                Console.WriteLine(reader.GetValue(0));
                x = (int)reader.GetValue(0);
            }

            myConnection.Close();

            //Relacionar talba Team_Match
            
            string query3 =
                          $"INSERT INTO [dbo].[Match_Team] ([id], [team_id], [match_id])" +
                          $"VALUES ('{x}', '{match.idTeam1}', '{match.MId}');" +
                          $"INSERT INTO [dbo].[Match_Team] ([id], [team_id], [match_id])" +
                          $"VALUES ('{x + 1}', '{match.idTeam2}', '{match.MId}');";

            SqlCommand sqlCmd3 = new SqlCommand(query3, myConnection);

            myConnection.Open();

            var created3 = sqlCmd3.ExecuteNonQuery();
            myConnection.Close();


            return created;

        }

        public async Task<int> updateTeam(Team newTeam)
        {

            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = 
                          $"UPDATE [dbo].[Team]" +
                          $"SET [tournamentId] = '{newTeam.TournamentId}'" + 
                          $"WHERE [te_id] = {newTeam.TeId}";

            Console.WriteLine("ASI SE VE EL QUERY");
            Console.WriteLine(query);

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();
            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;

        }

        public async Task<List<Tournament>> getTournaments()
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = "SELECT * FROM [dbo].[Tournament]";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<Tournament> tournaments = new List<Tournament>();

            while (reader.Read())
            {
                Tournament tournament = new Tournament();

                tournament.ToId = reader.GetValue(0).ToString();
                tournament.Name = reader.GetValue(1).ToString();
                tournament.StartDate = reader.GetValue(2).ToString();
                tournament.EndDate = reader.GetValue(3).ToString();
                tournament.Description = reader.GetValue(4).ToString();
                tournament.Type = reader.GetValue(5).ToString();
                
                tournaments.Add(tournament);

            }
            myConnection.Close();

            return tournaments;
        }

        public async Task<List<Tournament>> getTournamentsById(string id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                        $"FROM [dbo].[Tournament]" +
                        $"WHERE to_id = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<Tournament> tournaments = new List<Tournament>();

            while (reader.Read())
            {
                Tournament tournament = new Tournament();

                tournament.ToId = reader.GetValue(0).ToString();
                tournament.Name = reader.GetValue(1).ToString();
                tournament.StartDate = reader.GetValue(2).ToString();
                tournament.EndDate = reader.GetValue(3).ToString();
                tournament.Description = reader.GetValue(4).ToString();
                tournament.Type = reader.GetValue(5).ToString();

                tournaments.Add(tournament);

            }
            myConnection.Close();

            return tournaments;
        }



        public async Task<List<TeamWEB>> getTeamByTournamentId(string id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT [te_id], [name] " +
                $"FROM [dbo].[Team]" +
                $"WHERE tournamentId = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<TeamWEB> teams = new List<TeamWEB>();

            while (reader.Read())
            {
                TeamWEB newteam = new TeamWEB();

                newteam.TeId = (int) reader.GetValue(0);
                newteam.Name = reader.GetValue(1).ToString();

                teams.Add(newteam);

            }
            myConnection.Close();

            return teams;
        }



        public async Task<List<BracketWEB>> getBracketsByTournamentId(string id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Bracket]" +
                $"WHERE tournamentId = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<BracketWEB> brackets = new List<BracketWEB>();

            while (reader.Read())
            {
                BracketWEB newbracket = new BracketWEB();

                newbracket.BId = (int) reader.GetValue(0);
                newbracket.Name = reader.GetValue(1).ToString();
                newbracket.TournamentId = reader.GetValue(2).ToString();

                brackets.Add(newbracket);

            }
            myConnection.Close();

            return brackets;
        }



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


        public async Task<List<TeamWEB>> getTeamsByType(string type)
        {
            Console.WriteLine("GETTEAMSBYTYPE");
            Console.WriteLine(type);

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT [te_id], [name] " +
                $"FROM [dbo].[Team]" +
                $"WHERE [type] = '{type}' AND [tournamentId] IS NULL";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<TeamWEB> teams = new List<TeamWEB>();

            while (reader.Read())
            {
                TeamWEB team = new TeamWEB();

                team.TeId = (int) reader.GetValue(0);
                team.Name = reader.GetValue(1).ToString();
                
                teams.Add(team);

            }

            return teams;
        }

        public async Task<int> getTotalBrackets()
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = "SELECT * FROM [dbo].[Bracket]";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<Bracket> brackets = new List<Bracket>();

            while (reader.Read())
            {
                Bracket bracket = new Bracket();

                bracket.BId = (int) reader.GetValue(0);
                bracket.Name = reader.GetValue(1).ToString();
                bracket.TournamentId = reader.GetValue(2).ToString();
                

                brackets.Add(bracket);

            }

            int x = brackets.Count();

            myConnection.Close();

            return x;
        }

        public async Task<int> getTotalMatches()
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = "SELECT * FROM [dbo].[Match]";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<Match> matches = new List<Match>();

            while (reader.Read())
            {
                Match dbMatch = new Match();

                dbMatch.MId = (int)reader.GetValue(0);
                dbMatch.StartTime = reader.GetValue(1).ToString();
                dbMatch.Date = reader.GetValue(2).ToString();
                dbMatch.Venue = reader.GetValue(3).ToString();
                dbMatch.BracketId = (int)reader.GetValue(4);


                matches.Add(dbMatch);

            }

            int x = matches.Count();

            Console.WriteLine("COUNT DE BRACKETS");
            Console.WriteLine(x);

            myConnection.Close();

            return x;
        }

    }
}




