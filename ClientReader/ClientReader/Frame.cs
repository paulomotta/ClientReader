using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public class Frame
    {
        private byte header = 0x7D;
        private byte length;
        private byte code;
        private byte[] data;
        private byte checksum;

        public byte Header
        {
            get
            {
                return header;
            }
        }

        public byte Length
        {
            get
            {
                return length;
            }
        }

        public byte Code
        {
            get
            {
                return code;
            }
        }

        public byte[] Data
        {
            get
            {
                return data;
            }
        }

        public byte Checksum
        {
            get
            {
                return checksum;
            }
        }

        public Frame(byte length, byte code, byte[] data)
        {
            this.length = length;
            this.code = code;
            this.data = data;
            this.checksum = createChecksum();
        }

        public override string ToString()
        {
            return header + " " + length + " " + code + " " + data + " " + checksum;
        }

        public byte xorData()
        {
            byte xor = 0;
            if (data != null)
            {
                foreach (byte b in data)
                {
                    xor ^= b;
                }
            }
            return xor;
        }

        public byte createChecksum()
        {
            return (byte) (length ^ code ^ xorData());
        }
    }
}
