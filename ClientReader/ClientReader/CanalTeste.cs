using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public class CanalTeste : Canal
    {
        Mensagem msg;
        public override bool connect()
        {
            return true;
        }

        public override bool disconnect()
        {
            return true;
        }

        protected override Mensagem concreteReceive()
        {
            Mensagem m = null;
            switch (msg.Frame.Code)
            {
                case (byte)Frame.CODE.LerNumSerie:
                    m = Mensagem.createMensagemRespLerNumSerie("ABCDEFG\0");
                    break;
                case (byte)Frame.CODE.LerStatus:
                    m = Mensagem.createMensagemRespLerStatus(300, 600);
                    break;
                case (byte)Frame.CODE.DefinirRegistro:
                    m = Mensagem.createMensagemRespDefinirRegistro(0);
                    break;
                case (byte)Frame.CODE.LerDataHora:
                    byte[] data = { 0x7D, 0xE1, 0xBC, 0x59, 0x2B };
                    m = Mensagem.createMensagemRespLerDataHoraRegistroAtual(data);
                    break;

            }


            return m;
        }

        protected override bool concreteSend(Mensagem cmd)
        {
            msg = cmd;
            return true;
        }
    }
}
