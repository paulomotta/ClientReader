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
            byte []data = BitConverter.GetBytes(registro);
            Frame f = new Frame((byte)data.Length, (byte)Frame.CODE.DefinirRegistro, data);
            return new Mensagem(f);
        }
        
    }
}
