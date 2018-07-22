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
        private Mensagem currentResp;
        private Mensagem currentReq;

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
                    Mensagem msg = readMensagem();
                    if (msg == null)
                    {
                        break;
                    }
                    currentReq = msg;
                    Mensagem resp = null;
                    switch (msg.Frame.Code)
                    {
                        case (byte)Frame.CODE.LerNumSerie:
                            resp = Mensagem.createMensagemRespLerNumSerie("ABCDEFG\0");
                            break;
                        case (byte)Frame.CODE.LerStatus:
                            resp = Mensagem.createMensagemRespLerStatus(300, 600);
                            break;
                        case (byte)Frame.CODE.DefinirRegistro:
                            resp = Mensagem.createMensagemRespDefinirRegistro(0);
                            break;
                        case (byte)Frame.CODE.LerDataHora:
                            byte[] data = { 0x7D, 0xE1, 0xBC, 0x59, 0x2B };
                            resp = Mensagem.createMensagemRespLerDataHoraRegistroAtual(data);
                            break;
                        case (byte)Frame.CODE.LerValor:
                            resp = Mensagem.createMensagemRespLerValorRegistroAtual(10.0f);
                            break;
                        case (byte)Frame.CODE.Erro:
                            if (currentResp == null)
                            {
                                currentResp = Mensagem.createMensagemDeErro();
                            }
                            resp = currentResp;
                            break;

                    }

                    currentResp = resp;

                    writer.Write(resp.Frame.pack());
                }
                


            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public Mensagem readMensagem()
        {
            Mensagem m = null;
            try
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
                buffer[(size - 1) + 3 + 1] = checksum;

                byte[] tmp = new byte[size + 3 + 1];
                Array.Copy(buffer, tmp, (size + 3 + 1));

                m = Mensagem.createMensagem(new Frame(tmp));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return m;
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
