using System;
using System.Collections.Generic;

namespace WCO_Api.Models
{
    public partial class Prueba
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? TournamentId { get; set; }

        public virtual Tournament? Tournament { get; set; }
    }
}
