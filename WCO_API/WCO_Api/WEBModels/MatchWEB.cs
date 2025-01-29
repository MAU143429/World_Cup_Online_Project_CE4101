namespace WCO_Api.WEBModels
{
    public class MatchWEB
    {
        
        public string startTime { get; set; }

        public string date { get; set; }

        public string venue { get; set; }

        public int? scoreT1 { get; set; } = 0;

        public int? scoreT2 { get; set; } = 0;

        public int bracketId { get; set; }

        public int idTeam1 { get; set; }

        public int idTeam2 { get; set; }

    }
}
