using System;
using System.Collections.Generic;

namespace WCO_Api.Models
{
    public partial class Team
    {
        public Team()
        {
            MatchTeams = new HashSet<MatchTeam>();
            Players = new HashSet<Player>();
        }

        public int MId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? TournamentId { get; set; }

        public virtual Tournament? Tournament { get; set; }
        public virtual ICollection<MatchTeam> MatchTeams { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
