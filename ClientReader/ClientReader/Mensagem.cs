using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public class Mensagem
    {
        

        private Frame frame;

        public Frame Frame
        {
            get
            {
                return frame;
            }
        }

        private Mensagem(Frame f)
        {
            this.frame = f;
        }

        public static Mensagem createMensagemLerNumSerie()
        {
            Frame f = new Frame(0, (byte)Frame.CODE.LerNumSerie, null);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemRespLerNumSerie(string numSerie)
        {
            Encoding enc = Encoding.ASCII;
            byte[] bytes = enc.GetBytes(numSerie);
            Frame f = new Frame((byte)bytes.Length, (byte)Frame.CODE.RespLerNumSerie, bytes);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemLerStatus()
        {
            Frame f = new Frame(0, (byte)Frame.CODE.LerStatus, null);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemRespLerStatus(UInt16 antigo, UInt16 novo)
        {
            byte[] b_antigo = UInt16ToByteArray(antigo);
            byte[] b_novo = UInt16ToByteArray(novo);
            byte[] data = new byte[4];
            data[0] = b_antigo[0];
            data[1] = b_antigo[1];
            data[2] = b_novo[0];
            data[3] = b_novo[1];
            Frame f = new Frame((byte)4, (byte)Frame.CODE.RespLerStatus, data);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemDefinirRegistro(UInt16 registro)
        {
            byte[] data = UInt16ToByteArray(registro);
            Frame f = new Frame((byte)data.Length, (byte)Frame.CODE.DefinirRegistro, data);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemRespDefinirRegistro(byte errorCode)
        {
            byte[] data = { errorCode };
            Frame f = new Frame((byte)data.Length, (byte)Frame.CODE.RespDefinirRegistro, data);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemLerDataHoraRegistroAtual()
        {
            Frame f = new Frame(0, (byte)Frame.CODE.LerDataHora, null);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemLerValorRegistroAtual()
        {
            Frame f = new Frame(0, (byte)Frame.CODE.LerValor, null);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemDeErro()
        {
            Frame f = new Frame(0, (byte)Frame.CODE.Erro, null);
            return new Mensagem(f);
        }

        public static byte[] reverseByteArray(byte[] src)
        {
            int size = src.Length;
            byte[] dst = new byte[size];
            for (int i = 0; i < size; i++)
            {
                dst[i] = src[size - 1 - i];
            }
            return dst;
        }

        public static byte[] UInt16ToByteArray(UInt16 num)
        {
            return reverseByteArray(BitConverter.GetBytes(num));
        }

        public static UInt16 ByteArrayToUInt16(byte[] array)
        {
            return BitConverter.ToUInt16(reverseByteArray(array), 0);
        }

        public static byte[] FloatToIEEE754ByteArray(float num)
        {
            return reverseByteArray(BitConverter.GetBytes(num));
        }

        public static float IEEE754ByteArrayToFloat(byte[] array)
        {
            return BitConverter.ToSingle(reverseByteArray(array), 0);
        }

        public static string ByteArrayToDateTimeString(byte[] array)
        {
            UInt16 mesMask = 0x000F;

            byte[] anoMes = { array[1], array[0] };

            UInt16 ano = (UInt16)(BitConverter.ToUInt16(anoMes, 0) );
            UInt16 mes = (UInt16)(BitConverter.ToUInt16(anoMes, 0) & mesMask);
            ano >>= 4; 

            byte[] diaHoraMin = { array[3], array[2] };

            UInt16 diaMask  = 0xF800;
            UInt16 horaMask = 0x07C0;
            UInt16 minMask  = 0x003F;
            UInt16 dia = (UInt16)(BitConverter.ToUInt16(diaHoraMin, 0) & diaMask);
            UInt16 hora = (UInt16)(BitConverter.ToUInt16(diaHoraMin, 0) & horaMask);
            UInt16 min = (UInt16)(BitConverter.ToUInt16(diaHoraMin, 0) & minMask);
            dia >>= 11;
            hora >>= 6;

            UInt16 seg = (UInt16)(array[4] >> 2);
            
            DateTime dateTime = new DateTime(ano, mes, dia, hora, min, seg);

            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ByteArrayToString(byte[] array)
        {
            return Encoding.ASCII.GetString(array, 0, array.Length-1);
        }

        public override string ToString()
        {
            return frame.ToString();
        }

    }
}
