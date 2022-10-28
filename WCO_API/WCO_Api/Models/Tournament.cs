using System;
using System.Collections.Generic;

namespace WCO_Api.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Brackets = new HashSet<Bracket>();
            Teams = new HashSet<Team>();
        }

        public string ToId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual ICollection<Bracket> Brackets { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
