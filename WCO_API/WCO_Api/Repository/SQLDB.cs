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

    }
}




