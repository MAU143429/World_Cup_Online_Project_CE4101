using System;
using System.Collections.Generic;

namespace WCO_Api.Models
{
    public partial class Match
    {
        public Match()
        {
            MatchTeams = new HashSet<MatchTeam>();
        }

        public int MId { get; set; }
        public string StartTime { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Venue { get; set; } = null!;
        public int BracketId { get; set; }

        public virtual Bracket Bracket { get; set; } = null!;
        public virtual ICollection<MatchTeam> MatchTeams { get; set; }
    }
}
