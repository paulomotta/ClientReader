using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClientReader
{
    public class CanalSocket : Canal
    {
        private string ip;
        private int porta;
        private TcpClient conexao;

        public override bool connect()
        {
            return false;
        }

        public override bool disconnect()
        {
            return false;
        }

        public override bool concreteSend(Mensagem cmd)
        {
            return false;
        }

        public override Mensagem concreteReceive()
        {
            return null;
        }


    }
}
