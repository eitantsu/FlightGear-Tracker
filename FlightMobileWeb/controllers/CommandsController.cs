using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightMobileApp.models;
using System.Text;
using System.Net.Http;

namespace FlightMobileApp.controllers
{
    [Route("/api/command")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandContext _context;

        public CommandsController(CommandContext context)
        {
            _context = context;
        }


        // POST: api/Commands
        [HttpPost]
        public async Task<ActionResult<Command>> PostCommand(Command command)
        {
            if (ClientToSim.socket == null && command.Aileron == 0.123456789)
            {
                bool okconnect = Program.c.Connect();
                if (!okconnect) return StatusCode(500);
                return Ok(command);
            }
            else if (ClientToSim.socket != null && command.Aileron == 0.123456789)
            {
                if (!ClientToSim.socket.Connected)
                {
                    bool okconnect = Program.c.Connect();
                    if (!okconnect) return StatusCode(500);
                }
                return Ok(command);
            }

            command.Command_id = Guid.NewGuid().ToString("N").Substring(0, 7);
            // _context.Commands.Add(command);
            //await _context.SaveChangesAsync();
            bool res = await SendValues(command);
            if (res)
                return Ok(command);
            else return StatusCode(500);
        }
        public Task<bool> SendValues(Command command)
        {
            var taskcomplete = new TaskCompletionSource<bool>();
            bool ok = true;

            try
            {
                byte[] messageSent1 = Encoding.ASCII.GetBytes("set /controls/flight/aileron " + command.Aileron.ToString() + "\n");
                int byteSent1 = ClientToSim.socket.Send(messageSent1);
                byte[] messageSent11 = Encoding.ASCII.GetBytes("get /controls/flight/aileron\n");
                int byteSent11 = ClientToSim.socket.Send(messageSent11);
                byte[] messageReceived1 = new byte[1024];
                int byteRecv1 = ClientToSim.socket.Receive(messageReceived1);
                string answer1 = Encoding.ASCII.GetString(messageReceived1, 0, byteRecv1);
                if (!(Double.Parse(answer1) == command.Aileron)) ok = false;

                byte[] messageSent2 = Encoding.ASCII.GetBytes("set /controls/flight/elevator " + command.Elevator.ToString() + "\n");
                int byteSent2 = ClientToSim.socket.Send(messageSent2);
                byte[] messageSent22 = Encoding.ASCII.GetBytes("get /controls/flight/elevator\n");
                int byteSent22 = ClientToSim.socket.Send(messageSent22);
                byte[] messageReceived2 = new byte[1024];
                int byteRecv2 = ClientToSim.socket.Receive(messageReceived2);
                string answer2 = Encoding.ASCII.GetString(messageReceived2, 0, byteRecv2);
                if (!(Double.Parse(answer2) == command.Elevator)) ok = false;


                byte[] messageSent3 = Encoding.ASCII.GetBytes("set /controls/flight/rudder " + command.Rudder.ToString() + "\n");
                int byteSent3 = ClientToSim.socket.Send(messageSent3);
                byte[] messageSent33 = Encoding.ASCII.GetBytes("get /controls/flight/rudder\n");
                int byteSent33 = ClientToSim.socket.Send(messageSent33);
                byte[] messageReceived3 = new byte[1024];
                int byteRecv3 = ClientToSim.socket.Receive(messageReceived3);
                string answer3 = Encoding.ASCII.GetString(messageReceived3, 0, byteRecv3);
                if (!(Double.Parse(answer3) == command.Rudder)) ok = false;


                byte[] messageSent4 = Encoding.ASCII.GetBytes("set /controls/engines/current-engine/throttle " + command.Throttle.ToString() + "\n");
                int byteSent4 = ClientToSim.socket.Send(messageSent4);
                byte[] messageSent44 = Encoding.ASCII.GetBytes("get /controls/engines/current-engine/throttle\n");
                int byteSent44 = ClientToSim.socket.Send(messageSent44);
                byte[] messageReceived4 = new byte[1024];
                int byteRecv4 = ClientToSim.socket.Receive(messageReceived4);
                string answer4 = Encoding.ASCII.GetString(messageReceived4, 0, byteRecv4);
                if (!(Double.Parse(answer4) == command.Throttle)) ok = false;

                if (ok) taskcomplete.SetResult(true);
                else taskcomplete.SetResult(false);
            }

            catch (Exception e)
            {
                taskcomplete.SetException(e);
            }

            return taskcomplete.Task;
        }
    }
}
