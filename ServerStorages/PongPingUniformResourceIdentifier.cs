using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class PongPingUniformResourceIdentifier
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public int UsedConnections { get; set; }
        public string Path { get; set; } = null!;
        public DateTime Use { get; set; }
    }
}
