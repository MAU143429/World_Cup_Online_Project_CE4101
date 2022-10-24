namespace WCO_Api.Models
{
    public class Tournament
    {
        // Revisar como se tratará las varas de los ID, usando los UUID o como se hace esto en la 
        //base de datos
        public string? Id { get; set; }

        public string name { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public string description { get; set; }

        public string type { get; set; }

        public List<string> teams { get; set; }

        public List<string> brackets { get; set; }
    }
}
