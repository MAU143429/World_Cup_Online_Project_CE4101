using WCO_Api.WEBModels;

namespace WCO_Api.Logic
{
    public class ScoreCalculator
    {

        //Repositorios necesarios

        //Reglas de puntaje
        int exactMatchPts = 10;             //Rule 1
        int winnerMatchPts = 10;            //Rule 2
        int mvpMatchPts = 10;               //Rule 3
        int playerGoalPts = 5;              //Rule 4
        int playerAssistPts = 5;            //Rule 5

        bool guessAllGoals = true;
        bool guessAllAssists = true;

        //Método para calcular el puntaje
        public double calculatePts(PredictionWEB adminPredResult, PredictionWEB userPred)
        {

            double bonus = 0.75;

            int rule1Pts = finalScorePts(userPred.goalsT1, userPred.goalsT2, adminPredResult.goalsT1, adminPredResult.goalsT2);
            int rule2Pts = winnerPts(userPred.winner, adminPredResult.winner);
            int rule3Pts = mvpPts(userPred.PId, adminPredResult.PId);
            int rule4Pts = goalsPts(adminPredResult.predictionPlayers, userPred.predictionPlayers);
            int rule5Pts = asistsPts(adminPredResult.predictionPlayers, userPred.predictionPlayers);

            Console.WriteLine("Puntos R1: " + rule1Pts);
            Console.WriteLine("Puntos R2: " + rule2Pts);
            Console.WriteLine("Puntos R3: " + rule3Pts);
            Console.WriteLine("Puntos R4: " + rule4Pts);
            Console.WriteLine("Puntos R5: " + rule5Pts);

            if (rule1Pts != 0)
            {
                bonus += 0.25;
                Console.WriteLine("Activa bonus R1 ");
            }

            if (rule2Pts != 0)
            {
                bonus += 0.25;
                Console.WriteLine("Activa bonus R2 ");
            }

            if (rule3Pts != 0)
            {

                bonus += 0.25;
                Console.WriteLine("Activa bonus R3 ");
            }

            if (guessAllGoals)
            {
                bonus += 0.25;
                Console.WriteLine("Activa bonus R4 ");
            }

            if (guessAllAssists)
            {
                bonus += 0.25;
                Console.WriteLine("Activa bonus R5 ");
            }

            Console.WriteLine("Multiplicador al final: " + bonus);

            return bonus * (rule1Pts + rule2Pts + rule3Pts + rule4Pts + rule5Pts);
        }

        public int finalScorePts(int userGoalsT1, int userGoalsT2, int adminGoalsT1, int adminGoalsT2)
        {

            if (userGoalsT1 == adminGoalsT1 && userGoalsT2 == adminGoalsT2)
            {
                return exactMatchPts;
            }

            return 0;
        }

        public int winnerPts(int userWinner, int adminWinner)
        {

            if (userWinner == adminWinner)
            {
                return winnerMatchPts;
            }

            return 0;
        }

        public int mvpPts(int userMVP, int adminMVP)
        {

            if (userMVP == adminMVP)
            {
                return mvpMatchPts;
            }

            return 0;
        }

        public int goalsPts(List<PredictionPlayerWEB> adminPlayerList, List<PredictionPlayerWEB> userPlayerList)
        {

            int total = 0;

            //Pega puntos por acertar que queda 0 a 0 y no hay goles
            if (userPlayerList.Count() == 0 && adminPlayerList.Count() == 0)
            {
                return total;
            }

            if (userPlayerList.Count() == 0 || adminPlayerList.Count() == 0)
            {
                guessAllGoals = false;
                return total;
            }

            foreach (var adminPlayer in adminPlayerList)
            {
                foreach (var userPlayer in userPlayerList)
                {

                    //adinPlayer es alguien

                    //Es un jugador que mete goles, lo tomamos en cuenta
                    if (adminPlayer.goals > 0)
                    {
                        
                        //Si el usuario dice que mete algun gol
                        if (adminPlayer.PId == userPlayer.PId)
                        {

                            //Si el jugador predice más goles de un mismo jugador que el admin
                            if (userPlayer.goals > adminPlayer.goals)
                            {
                                total += adminPlayer.goals;
                                guessAllGoals = false;
                                
                            }
                            else if (userPlayer.goals == adminPlayer.goals)
                            {
                                total += adminPlayer.goals;
                                guessAllGoals = true;
                                
                            }
                            else 
                            {
                                total += userPlayer.goals;
                                guessAllGoals = false;
                                
                            }
                            break;
                        }
                        
                        else
                        {
                            guessAllGoals = false;
                        }
                    } 

                }
            }

            //goles acertados se multiplican por el marcador
            return total * playerGoalPts;
        }

        public int asistsPts(List<PredictionPlayerWEB> adminPlayerList, List<PredictionPlayerWEB> userPlayerList)
        {

            int total = 0;

            //Pega puntos por acertar que queda 0 a 0 y no hay asistencias
            if (userPlayerList.Count() == 0 && adminPlayerList.Count() == 0)
            {
                return total;
            }

            if (userPlayerList.Count() == 0 || adminPlayerList.Count() == 0)
            {
                guessAllAssists = false;
                return total;
            }

            foreach (var adminPlayer in adminPlayerList)
            {
                foreach (var userPlayer in userPlayerList)
                {
                    //Es un jugador que hace asistencias, lo tomamos en cuenta
                    if (adminPlayer.assists > 0)
                    {

                        //Si el usuario dice que hace alguna asistencia
                        if (adminPlayer.PId == userPlayer.PId)
                        {

                            //Si el jugador predice más asistencias de un mismo jugador que el admin
                            if (userPlayer.assists > adminPlayer.assists)
                            {
                                total += adminPlayer.assists;
                                guessAllAssists = false;
                            }
                            else if (userPlayer.assists == adminPlayer.assists)
                            {
                                total += adminPlayer.assists;
                                guessAllAssists = true;
                            }
                            else
                            {
                                total += userPlayer.assists;
                                guessAllAssists = false;
                            }
                            break;
                        }
                        else
                        {
                            guessAllAssists = false;
                        }
                    }

                }
            }

            return total * playerAssistPts;

        }

    }
}
