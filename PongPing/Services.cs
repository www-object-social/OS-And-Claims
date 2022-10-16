using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
namespace PongPing;
[EnableCors("signalr_policy")]
public class Services:Hub
{
}
