using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class UnitUser
    {
        public Guid Id { get; set; }
        public Guid UnitIdentificationId { get; set; }
        public bool IsActive { get; set; }

        public virtual UnitIdentification UnitIdentification { get; set; } = null!;
    }
}
