using System.Threading.Tasks;

namespace PsProtocol
{
    public interface IPsOutput
    {
        Task Send(byte data);
    }
}