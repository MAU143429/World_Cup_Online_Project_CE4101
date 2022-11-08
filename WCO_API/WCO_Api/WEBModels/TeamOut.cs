using System;
using System.Collections.Generic;

namespace WCO_Api.WEBModels
{
    public partial class TeamOut
    {

        public int TeId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? TournamentId { get; set; }


    }
}
