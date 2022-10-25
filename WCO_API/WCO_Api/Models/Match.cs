namespace WCO_Api.Models
{
    public class Match
    {
        public string Id { get; set; }

        public string tournamentName { get; set; }

        public string phaseName { get; set; }

        public string venue { get; set; }

        public DateTime startDate { get; set; }

        public string team1 { get; set; }

        public string team2 { get; set; }
    }
}
