using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public class Frame
    {
        public enum FIELDS : int { HEADER, LENGTH, CODE, DATA};
        public enum CODE : byte
        {
            LerNumSerie = 0x01,
            RespLerNumSerie = 0x81,
            LerStatus = 0x02,
            RespLerStatus = 0x82,
            DefinirRegistro = 0x03,
            RespDefinirRegistro = 0x83,
            LerDataHora = 0x04,
            RespLerDataHora = 0x84,
            LerValor = 0x05,
            RespLerValor = 0x85,
            Erro = 0xFF,
        }
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

        public Frame(byte[] packedData)
        {
            this.header = packedData[(int)FIELDS.HEADER];
            this.length = packedData[(int)FIELDS.LENGTH];
            this.code = packedData[(int)FIELDS.CODE];
            if (this.length > 0)
            {
                byte[] tmp = new byte[this.length];
                Array.Copy(packedData, (int)FIELDS.DATA, tmp, 0, this.length);
                this.data = tmp;
            } else
            {
                this.data = null;
            }
            this.checksum = packedData[(int)FIELDS.DATA + this.length];
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

        public bool isValidChecksum()
        {
            return (createChecksum() == this.checksum);
        }

        public static bool matchCodes(Frame req, Frame resp)
        {
            byte reqCode = req.code;
            byte respCode = resp.code;

            return (reqCode == (respCode - 128));
        }

        public int frameSize()
        {
            int size = 4;
            if (data != null) { size += data.Length; }
            return size;
        }

        public byte[] pack()
        {
            byte[] tmp = new byte[frameSize()];
            tmp[(int)FIELDS.HEADER] = header;
            tmp[(int)FIELDS.LENGTH] = length;
            tmp[(int)FIELDS.CODE] = code;
            if (data != null)
            {
                Array.Copy(data, 0, tmp, (int)FIELDS.DATA, length);
            }
            tmp[(int)FIELDS.DATA + length] = checksum;
            return tmp;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Frame f = (Frame)obj;

            if (data != null)
            {
                if (f.data == null) return false;
                if (length != f.length) return false;

                for (int i=0; i < length; i++)
                {
                    if (data[i] != f.data[i]) return false;
                }
            }

            return (header == f.header) && 
                (length == f.length) &&
                (code == f.code) &&
                checksum == f.checksum;
        }

        public override int GetHashCode()
        {
            return length+code+checksum;
        }
    }
}
