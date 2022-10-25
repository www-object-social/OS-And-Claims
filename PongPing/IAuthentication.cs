using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PongPing;
public interface IAuthentication
{
	public Task Create(string ConnectionID, string Host);
}
