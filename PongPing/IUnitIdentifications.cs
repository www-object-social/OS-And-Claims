using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongPing;
public interface IUnitIdentifications
{
    Task Verify(string ConnectionID,string Host, string Token, string ISO639_1, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN);
    Task Create(string ConnectionID, string Host, string ISO639_1, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN);
}
