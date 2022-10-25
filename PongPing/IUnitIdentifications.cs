using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongPing;
public interface IUnitIdentifications
{
    Task Verify(string ConnectionID,System.Net.IPAddress RemoteIpAddress, string Host, string Token, string ISO639_1,string ISO3166, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN,int BaseUtcOffsetTotalMinutes);
    Task Create(string ConnectionID, System.Net.IPAddress RemoteIpAddress, string Host, string ISO639_1, string ISO3166, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN, int BaseUtcOffsetTotalMinutes);
    Task Remove(string ConnectionID, string Host);
    Task<Guid> GetID(string ConnectionID, string Host);
	Task ISO3166(string ConnectionID, string Host, string Value);
	Task UpdateUI();
	string GetISO3166 { get; }
	string GetISO639_1 { get; }
}
