using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class TokenSecurity
    {
        public Guid Code { get; set; }
        public Guid TokenId { get; set; }
        public DateTime Created { get; set; }

        public virtual Token Token { get; set; } = null!;
    }
}
