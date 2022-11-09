using Microsoft.Data.SqlClient;
using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    public class PredictionDatabase
    {

        string CONNECTION_STRING;

        public PredictionDatabase()
        {
            CONNECTION_STRING = "Data Source=localhost;Initial Catalog=WCODB;Integrated Security=True";
        }

        public async Task<int> insertPrediction(PredictionWEB prediction)
        {


            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            //Hace el insert a la tabla de partidos

            string query =
                          $"INSERT INTO [dbo].[Prediction] ([goalsT1], [goalsT2], [points], [player_id], [acc_nick], [acc_email], [match_id] )" +
                          $"VALUES ('{prediction.goalsT1}', '{prediction.goalsT2}', '{prediction.points}', '{prediction.PId}', '{prediction.acc_nick}', '{prediction.acc_email}', '{prediction.match_id}');" +
                          $"SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            int thisPredId = 0;

            while (reader.Read())
            {
                thisPredId = Decimal.ToInt32((decimal)reader.GetValue(0));
            }

            Console.WriteLine("EL ID DE LA PREDICCION QUE ACABA DE HACER ES");
            Console.WriteLine(thisPredId);

            myConnection.Close();

            return thisPredId;
        }

        public async Task<int> insertPredictionPlayer(PredictionPlayerWEB predPlayer)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            //Hace el insert a la tabla de partidos

            string query =
                          $"INSERT INTO [dbo].[Scores_Assists] ([player_id], [prediction_id], [assists], [goals])" +
                          $"VALUES ('{predPlayer.PId}', '{predPlayer.PId}', '{predPlayer.assists}', '{predPlayer.goals}');";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;
        }
    }
}
