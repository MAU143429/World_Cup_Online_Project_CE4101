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

            SqlTransaction transaction = null;
            SqlConnection myConnection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {

                myConnection = new SqlConnection(CONNECTION_STRING);

                myConnection.Open();

                //Start the transaction
                transaction = myConnection.BeginTransaction();

                string query =
                          $"INSERT INTO [dbo].[Prediction] ([goalsT1], [goalsT2], [winner], [points], [player_id], [acc_nick], [acc_email], [match_id] )" +
                          $"VALUES ('{prediction.goalsT1}', '{prediction.goalsT2}', '{prediction.winnerId}','{prediction.points}', '{prediction.PId}', '{prediction.acc_nick}', '{prediction.acc_email}', '{prediction.match_id}');" +
                          $"SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];";

                command = new SqlCommand(query, myConnection);

                //assosiate the command-variable with the transaction
                command.Transaction = transaction;
                //Se inserta a la tabla predicciones la prediccion general, y se revisa que id fue la prediccion que se acaba de hacer
                
                reader = command.ExecuteReader();

                int thisPredId = 0;

                while (reader.Read())
                {
                    thisPredId = Decimal.ToInt32((decimal)reader.GetValue(0));
                }

                Console.WriteLine("EL ID DE LA PREDICCION QUE ACABA DE HACER ES");
                Console.WriteLine(thisPredId);

                reader.Close();

                //Añado a cada uno de los jugadores de la prediccion con sus respectivos goles y asistencias
                
                foreach (var predPlayer in prediction.predictionPlayers)
                {
                    predPlayer.PrId = thisPredId;        //Se le pone el id de la predicción que se acaba de hacer

                    string query2 =
                          $"INSERT INTO [dbo].[Scores_Assists] ([player_id], [prediction_id], [assists], [goals])" +
                          $"VALUES ('{predPlayer.PId}', '{predPlayer.PrId}', '{predPlayer.assists}', '{predPlayer.goals}');";

                    command = new SqlCommand(query2, myConnection);

                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                }

                //Cuando se hace una predicción, la tabla torneo puntaje se llena con el id del torneo, usuario y el puntaje
                //Ver group_id de predicción, que procede con eso
                string query3 = $"INSERT INTO [dbo].[Tournament_Account_S] ([t_id], [acc_nick], [acc_email], [points], [group_id] )" +
                          $"VALUES ('{prediction.TId}', '{prediction.acc_nick}', '{prediction.acc_email}', '{prediction.points}', NULL);";

                command = new SqlCommand(query3, myConnection);

                command.Transaction = transaction;
                command.ExecuteNonQuery();

                transaction.Commit();

                return 1;
            }
            catch (Exception error)
            {
                reader.Close();
                transaction.Rollback();
                Console.WriteLine(error);
                return -1;
            }
            finally
            {
                myConnection.Close();
            }

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
                prediction.winnerId = (int)reader.GetValue(3);
                prediction.points = (int)reader.GetValue(4);
                prediction.PId = (int)reader.GetValue(5);
                prediction.acc_nick = reader.GetValue(6).ToString();
                prediction.acc_email = reader.GetValue(7).ToString();
                prediction.match_id = (int)reader.GetValue(8);

            }

            myConnection.Close();

            //Ahora, teniendo el id de la predicción hay que armar los jugadores anotadores y asistentes

            string query2 = $"SELECT * " +
                $"FROM [dbo].[Scores_Assists]" +
                $"WHERE prediction_id = '{prediction.PrId}'";

            SqlCommand sqlCmd2 = new SqlCommand(query2, myConnection);

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
