using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class TokensSecurity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public byte[] Value0 { get; set; } = null!;
        public byte[] Value1 { get; set; } = null!;
        public DateTime AutomaticDeletion { get; set; }
    }
}
