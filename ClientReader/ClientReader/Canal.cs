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

        public abstract bool concreteSend(Mensagem cmd);

        public abstract Mensagem concreteReceive();

        public Mensagem processRequest(Mensagem cmd)
        {
            send(cmd);
            Mensagem m = receive();
            while(m == null)
            {
                m = receive();
            }

            return m;
        }
        public bool send(Mensagem cmd)
        {
            currentRequest = cmd;
            return concreteSend(cmd);
        }

        public Mensagem receive()
        {
            Mensagem msg = concreteReceive();
            if(msg.Frame.Code == (byte)Frame.CODE.Erro)
            {
                send(currentRequest);
                return null;
            }
            currentResponse = msg;
            //TODO may become an infinite loop, add a max retries counter
            while (!validaRetorno(msg))
            {
                Mensagem error = Mensagem.createMensagemDeErro();
                concreteSend(error);
                msg = concreteReceive();
                currentResponse = msg;
            }
            return msg;

        }

        private bool validaRetorno(Mensagem msg)
        {
            return msg.Frame.isValidChecksum() && 
                Frame.matchCodes(currentRequest.Frame,msg.Frame) ;
        }
    }
}
