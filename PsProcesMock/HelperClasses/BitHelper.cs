using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.HelperClasses
{
    class BitHelper
    {
        public static bool FromBit(byte b, int index)
        {
            return (((1 << index) & b) != 0);
        }
        public static byte AddBit(byte inByte, int index, bool value)
        {
            if (value)
            {
                return (byte)((1 << index) | inByte);
            }
            else
            {
                return (byte)((~(1 << index)) & inByte);
            }
        }
    }
}
