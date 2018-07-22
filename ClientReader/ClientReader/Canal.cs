using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public abstract class Canal
    {
        private Mensagem currentRequest;
        private Mensagem currentResponse;

        public abstract bool connect();

        public abstract bool disconnect();

        protected abstract bool concreteSend(Mensagem cmd);

        protected abstract Mensagem concreteReceive();

        public bool send(Mensagem cmd)
        {
            currentRequest = cmd;
            return concreteSend(cmd);
        }

        public Mensagem receive()
        {
            Mensagem msg = concreteReceive();
            currentResponse = msg;
            if (validaRetorno(msg))
            {
                return msg;
            } else
            {
                return null;
            }
            
        }

        private bool validaRetorno(Mensagem msg)
        {
            throw new NotImplementedException();
        }
    }
}
