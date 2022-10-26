using System;
using System.Collections.Generic;

namespace WCO_Api.Models
{
    public partial class Bracket
    {
        public Bracket()
        {
            Matches = new HashSet<Match>();
        }

        public int BId { get; set; }
        public string Name { get; set; } = null!;
        public string TournamentId { get; set; } = null!;

        public virtual Tournament Tournament { get; set; } = null!;
        public virtual ICollection<Match> Matches { get; set; }
    }
}
