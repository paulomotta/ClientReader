using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

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

        public override bool concreteSend(Mensagem cmd)
        {
            Frame f = cmd.Frame;
            try
            {
                writer.Write(f.pack());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
            return true;
        }

        public override Mensagem concreteReceive()
        {
            byte[] buffer = new byte[260];
            //Header, Size, Code
            for (int i = 0; i < 3; i++)
            {
                buffer[i] = reader.ReadByte();
            }

            //Data
            int size = buffer[1];
            for (int i = 0; i < size; i++)
            {
                buffer[i + 3] = reader.ReadByte();
            }

            //Checksum
            byte checksum = reader.ReadByte();
            buffer[(size-1) + 3 + 1] = checksum;
            Debug.WriteLine("checksum=" +checksum);

            byte[] tmp = new byte[size + 3 + 1];
            Array.Copy(buffer, tmp, (size + 3 + 1));

            Mensagem m = Mensagem.createMensagem(new Frame(tmp));
            return m;
        }


    }
}
