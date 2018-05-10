using PsProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsClient
{
    public class PsClientConfiguration
    {
        public string Address { get; }
        public int Port { get; }
        public Func<IPsOutput, byte, Task> ReceiveCallback { get; }

        public PsClientConfiguration(string address, int port, Func<IPsOutput, byte, Task> receiveCallback)
        {
            Address = address;
            Port = port;
            ReceiveCallback = receiveCallback;
        }

    }
}
