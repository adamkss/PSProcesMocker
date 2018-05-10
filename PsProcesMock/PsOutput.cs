using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsProtocol
{
    public class PsOutput : IPsOutput
    {
        public PsOutput(Stream stream)
        {
            _stream = stream;
        }

        public Task Send(byte data)
        {
            var buffer = new byte[] { data };
            return _stream.WriteAsync(buffer, 0, 1);
        }

        private Stream _stream;
    }
}
