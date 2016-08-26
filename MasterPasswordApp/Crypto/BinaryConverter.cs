using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterPasswordApp.Crypto
{
    class BinaryConverter
    {
        public static byte[] BytesForInt(int length)
        {
            uint uLength = Convert.ToUInt32(length);
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(uLength);
            }
            byte[] littleEndianBytes = stream.ToArray();
            byte[] bigEndianBytes = littleEndianBytes.Reverse().ToArray();
            return bigEndianBytes;
        }
    }
}
