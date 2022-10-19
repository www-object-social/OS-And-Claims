using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class UnitConnection
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = null!;
        public string Host { get; set; } = null!;
        public Guid UnitIdentificationId { get; set; }

        public virtual UnitIdentification UnitIdentification { get; set; } = null!;
    }
}
