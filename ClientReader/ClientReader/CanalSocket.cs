using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ClientReader
{
    public class CanalSocket : Canal
    {
        private string ip;
        private int porta;
        private TcpClient conexao;
        private NetworkStream stream;
        private BinaryWriter writer;
        private BinaryReader reader;

        public CanalSocket(string ip, int porta)
        {
            this.ip = ip;
            this.porta = porta;
        }

        public override bool connect()
        {
            try
            {
                conexao = new TcpClient();
                conexao.Connect(ip, porta);
                stream = conexao.GetStream();
                writer = new BinaryWriter(stream);
                reader = new BinaryReader(stream);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
                if (writer != null)
                {
                    writer.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
                if (conexao != null)
                {
                    conexao.Close();
                }
                return false;
            }
            return true;
        }

        public override bool disconnect()
        {
            try
            {
                if (writer != null)
                {
                    writer.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
                if (conexao != null)
                {
                    conexao.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
                return false;
            }
            return true;
        }

        protected override bool concreteSend(Mensagem cmd)
        {
            return false;
        }

        protected override Mensagem concreteReceive()
        {
            return null;
        }


    }
}
