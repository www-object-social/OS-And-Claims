using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongPing.uniformResource
{
    public class Identifier
    {
        public readonly string Path;
        public readonly int LimitOfConnection;
        public Identifier(string Path,int LimitOfConnection)
        {
            this.Path = Path;
            this.LimitOfConnection = LimitOfConnection;
        }   
    }
}
