using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientReader
{
    public class EchoServer
    {
        private Socket socket;
        private TcpListener conexao;
        private NetworkStream stream;
        private BinaryWriter writer;
        private BinaryReader reader;
        private int porta;
        private byte[] buffer = new byte[260];

        public EchoServer(int porta)
        {
            this.porta = porta;
           
        }

        public void init()
        {
            try
            {
                conexao = new TcpListener(porta);
                conexao.Start();
                socket = conexao.AcceptSocket();
                stream = new NetworkStream(socket);
                writer = new BinaryWriter(stream);
                reader = new BinaryReader(stream);

                while (true)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        buffer[i] = reader.ReadByte();
                        Console.WriteLine("EchoServer>>" + buffer[i]);
                    }
                    
                    for (int i = 0; i < 4; i++)
                    {
                        writer.Write(buffer[i]);
                    }
                }
                


            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void stop()
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
            if (socket != null)
            {
                socket.Close();
            }
            if (conexao != null)
            {
                conexao.Stop();
            }
        }
    }
}
