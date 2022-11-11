namespace WCO_Api.WEBModels
{
    public class PredictionWEB
    {
        public int? PrId { get; set; }

        public int goalsT1 { get; set; }

        public int goalsT2 { get; set; }

        public int? points { get; set; } = 0;

        public int PId { get; set; }

        public string acc_nick { get; set; }

        public string acc_email { get; set; }

        public int match_id { get; set; }

        public List<PredictionPlayerWEB> predictionPlayers { get; set; }

    }
}


