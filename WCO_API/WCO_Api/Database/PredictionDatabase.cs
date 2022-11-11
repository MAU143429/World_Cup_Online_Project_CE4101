﻿using Microsoft.Data.SqlClient;
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
                          $"VALUES ('{predPlayer.PId}', '{predPlayer.PrId}', '{predPlayer.assists}', '{predPlayer.goals}');";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;
        }

        public async Task<PredictionWEB> getPredictionByNEM(string nickname, string email, int idMatch)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Prediction]" +
                $"WHERE acc_nick = '{nickname}' and acc_email = '{email}' and match_id = '{idMatch}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            PredictionWEB prediction = new PredictionWEB();

            while (reader.Read())
            {

                //Obtener info de la predicción en si
                prediction.PrId = (int)reader.GetValue(0);
                prediction.goalsT1 = (int)reader.GetValue(1);
                prediction.goalsT2 = (int)reader.GetValue(2);
                prediction.points = (int)reader.GetValue(3);
                prediction.PId = (int)reader.GetValue(4);
                prediction.acc_nick = reader.GetValue(5).ToString();
                prediction.acc_email = reader.GetValue(6).ToString();
                prediction.match_id = (int)reader.GetValue(7);

            }

            myConnection.Close();

            //Ahora, teniendo el id de la predicción hay que armar los jugadores anotadores y asistentes

            string query2 = $"SELECT * " +
                $"FROM [dbo].[Scores_Assists]" +
                $"WHERE prediction_id = '{prediction.PrId}'";

            SqlCommand sqlCmd2 = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd2.ExecuteReader();

            List<PredictionPlayerWEB> predictionPlayers = new  List<PredictionPlayerWEB>();

            while (reader.Read())
            {

                PredictionPlayerWEB predictionPlayer = new PredictionPlayerWEB();

                //Obtener info de la predicción en si
                predictionPlayer.PrId = prediction.PrId;
                predictionPlayer.PId = (int)reader.GetValue(0);
                predictionPlayer.assists = (int)reader.GetValue(2);
                predictionPlayer.goals = (int)reader.GetValue(3);

                predictionPlayers.Add(predictionPlayer);
            }

            myConnection.Close();

            prediction.predictionPlayers = predictionPlayers;

            return prediction;

        }

    }
}
