using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class UnitUser
    {
        public Guid Id { get; set; }
        public Guid UnitIdentificationId { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public byte[] VerdificationCode { get; set; } = null!;
        public bool IsBlock { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public Guid UserId { get; set; }

        public virtual UnitIdentification UnitIdentification { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
