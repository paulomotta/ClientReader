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
            return Mensagem.createMensagemRespLerNumSerie("ABCDEFG\0");
        }

        protected override bool concreteSend(Mensagem cmd)
        {
            msg = cmd;
            return true;
        }
    }
}
