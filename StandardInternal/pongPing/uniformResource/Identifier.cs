using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardInternal.pongPing.uniformResource;
public class Identifier : IIdentifier
{
    public string Path { get; set; }
    public int LimitOfConnection { get; set; }
    public DateTime Created { get; set; }
    public DateTime Use { get; set; }
    public Guid ID { get; set; }
    public int UsedConnections { get; set; }

}
