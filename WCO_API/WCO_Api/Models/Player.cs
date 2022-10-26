using System;
using System.Collections.Generic;

namespace WCO_Api.Models
{
    public partial class Player
    {
        public int PId { get; set; }
        public string Name { get; set; } = null!;
        public int TeamId { get; set; }

        public virtual Team Team { get; set; } = null!;
    }
}
