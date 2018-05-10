using PsProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsClient
{
    public class PsClientApp
    {
        public IPsOutput PsOutput { get; private set; }
        public PsClientApp(PsClientConfiguration configuration)
        {
            _configuration = configuration;
            _cancellationSource = new CancellationTokenSource();
        }
        /*
        public async Task Start()
        {
            CreateTcpClient();
            await ProcessData();
        }*/
        public void Start()
        {
            CreateTcpClient();
            //await ProcessData();
        }
        public void Stop()
        {
            _cancellationSource.Cancel();
            _stream.Close();
            _client.Close();
        }

        private void CreateTcpClient()
        {
            _client = new TcpClient(_configuration.Address, _configuration.Port);
            _stream = _client.GetStream();
            PsOutput = new PsOutput(_stream);
        }

        private async Task ProcessData()
        {
            var data = new byte[1];
            try
            {
                while (!_cancellationSource.IsCancellationRequested)
                {
                    await ReadOneByteAndProcessIt(data);
                }
            }
            catch(ObjectDisposedException ex)
            {
                Console.WriteLine($"Client was closed. {ex.Message}");
            }
        }

        private async Task ReadOneByteAndProcessIt(byte[] data)
        {
            var count = await _stream.ReadAsync(data, 0, 1, _cancellationSource.Token);
            if (count == 1)
            {
                await _configuration.ReceiveCallback(PsOutput, data[0]);
            }
        }

        private PsClientConfiguration _configuration;
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly CancellationTokenSource _cancellationSource;
    }
}
