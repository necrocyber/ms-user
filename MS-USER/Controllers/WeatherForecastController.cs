using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using System.Net.WebSockets;
using System.IO;
using System.Text;
using System.Security.Policy;
using WebSocketSharp;
using WebSocket = WebSocketSharp.WebSocket;

namespace MS_USER.Controllers
{
    public class UserType
    {
        public string dni { get; set; }
    }

    public class Message
    {
        public string state { get; set; }
        public string dni { get; set; }
        public string action { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string Connection = "ws://websocket.amedel-zen.workers.dev/api/rooms/websocket/user";


        [HttpPost]
        [Route("close_session")]
        public String Post([FromBody] UserType body)
        {
            using (var ws = new WebSocket(Connection))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine(e.Data);

                ws.Connect();

                var closeMessage = new Message
                {
                    state = "message",
                    dni = body.dni,
                    action = "close"
                };
                ws.Send(JsonConvert.SerializeObject(closeMessage));
                //Console.Read(true);
            }

            return "Socket enviado..";


        }
    }
}

