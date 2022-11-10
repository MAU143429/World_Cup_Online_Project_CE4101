namespace WCO_Api.WEBModels
{
    public class MatchOut
    {
        public int MId { get; set; }

        public string startTime { get; set; }

        public string date { get; set; }

        public string venue { get; set; }

        public int scoreT1 { get; set; }

        public int scoreT2 { get; set; }

        public int bracketId { get; set; }

        public List<TeamWEB> teams {get; set;}
    }
}
