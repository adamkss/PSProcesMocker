using PsClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class Client
    {
        private static PsClientApp PsClientAppInstance;
        static Client()
        {
            
            var configuration = new PsClientConfiguration("127.0.0.1", 13000, (output, data) => {
                Console.WriteLine($"Received {data.ToString()}");
                return Task.CompletedTask;
            });

            PsClientAppInstance = new PsClientApp(configuration);
            PsClientAppInstance.Start();
        }

        public static async Task sendByte(byte b)
        {
            await PsClientAppInstance.PsOutput.Send(b);
        }

      
    }
}
