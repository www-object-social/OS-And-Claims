using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class UnitIdentification
    {
        public UnitIdentification()
        {
            UnitConnections = new HashSet<UnitConnection>();
            UnitUsers = new HashSet<UnitUser>();
        }

        public Guid Id { get; set; }
        public Guid? TokenId { get; set; }
        public DateTime Created { get; set; }
        public int SuiT { get; set; }
        public DateTime AutomaticDeletion { get; set; }
        public int SpiN { get; set; }
        public string Iso6391 { get; set; } = null!;
        public string Iso3166 { get; set; } = null!;
        public int BaseUtcOffsetTotalMinutes { get; set; }

        public virtual Token? Token { get; set; }
        public virtual ICollection<UnitConnection> UnitConnections { get; set; }
        public virtual ICollection<UnitUser> UnitUsers { get; set; }
    }
}
