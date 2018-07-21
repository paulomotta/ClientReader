using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public class Medidor
    {
        private string ip;
        private int porta;

        public bool connect()
        {
            return false;
        }

        public bool disconnect()
        {
            return false;
        }

        public bool sendCommand(Mensagem cmd)
        {
            return false;
        }

        public Mensagem receive()
        {
            Mensagem msg = null;
            validaRetorno(msg);
            return null;
        }

        private void validaRetorno(Mensagem msg)
        {
            throw new NotImplementedException();
        }
    }
}
