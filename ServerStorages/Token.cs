using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class Token
    {
        public Token()
        {
            TokenSecurities = new HashSet<TokenSecurity>();
            UnitIdentifications = new HashSet<UnitIdentification>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime AutomaticDeletion { get; set; }

        public virtual ICollection<TokenSecurity> TokenSecurities { get; set; }
        public virtual ICollection<UnitIdentification> UnitIdentifications { get; set; }
    }
}
