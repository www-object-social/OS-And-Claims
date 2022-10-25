using System;
using System.Collections.Generic;

namespace ServerStorages
{
    public partial class User
    {
        public User()
        {
            UnitUsers = new HashSet<UnitUser>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public int ExpiresNumberOfDays { get; set; }
        public int ExpiresNumberOfMonths { get; set; }
        public int ExpiresNumberOfYears { get; set; }
        public DateTime Expires { get; set; }
        public string Iso6391 { get; set; } = null!;
        public string Iso3166 { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<UnitUser> UnitUsers { get; set; }
    }
}
