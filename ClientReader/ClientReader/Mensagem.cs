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

        public static Mensagem createMensagemLerStatus()
        {
            Frame f = new Frame(0, (byte)Frame.CODE.LerStatus, null);
            return new Mensagem(f);
        }

        public static Mensagem createMensagemDefinirRegistro(UInt16 registro)
        {
            byte[] data = UInt16ToByteArray(registro);
            Frame f = new Frame((byte)data.Length, (byte)Frame.CODE.DefinirRegistro, data);
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

        public static byte[] UInt16ToByteArray(UInt16 num)
        {
            byte[] tmp = BitConverter.GetBytes(num);
            int size = tmp.Length;
            byte[] data = new byte[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = tmp[size - 1 - i];
            }
            return data;
        }

        public static UInt16 ByteArrayToUInt16(byte[] array)
        {
            int size = array.Length;
            byte[] data = new byte[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = array[size - 1 - i];
            }

            UInt16 num = BitConverter.ToUInt16(data,0);
            return num;
        }
        
    }
}
