namespace WCO_Api.WEBModels
{
    public class TournamentWEB
    {
        public string? ToId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public List<int> teams { get; set; }
        public List<String> brackets { get; set; }

    }
}
