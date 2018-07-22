using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public class Medidor
    {
        private Canal canal;

        public Medidor(Canal c)
        {
            this.canal = c;
            bool result = canal.connect();
        }

        public string lerNumSerie()
        {
            Mensagem msg = Mensagem.createMensagemLerNumSerie();
            //Console.WriteLine(msg);
            Mensagem response = canal.processRequest(msg);
            //Console.WriteLine(response);
            string numSerie = Mensagem.ByteArrayToString(response.Frame.Data);
            //Console.WriteLine(numSerie);
            return numSerie;
        }
        public bool lerRegistroStatus()
        {
            Mensagem msg = Mensagem.createMensagemLerStatus();
            Console.WriteLine(msg);
            Mensagem response = canal.processRequest(msg);
            Console.WriteLine(response);
            byte []data = response.Frame.Data;
            byte[] antigo = new byte[2];
            byte[] novo = new byte[2];

            antigo[0] = data[0];
            antigo[1] = data[1];
            novo[0] = data[2];
            novo[1] = data[3];

            UInt16 regAntigo = Mensagem.ByteArrayToUInt16(antigo);
            UInt16 regNovo = Mensagem.ByteArrayToUInt16(novo);

            Console.WriteLine(regAntigo + " " + regNovo);
            return true;
        }
        public byte definirIndiceLeitura(UInt16 indice)
        {
            Mensagem msg = Mensagem.createMensagemDefinirRegistro(indice);
            Console.WriteLine(msg);
            Mensagem response = canal.processRequest(msg);
            Console.WriteLine(response);
            return response.Frame.Data[0];

        }
        public void lerDataHoraRegistroAtual()
        {

        }
        public void lerValorEnergiaRegistroAtual()
        {

        }
        public void executarLeitura()
        {
            lerNumSerie();
        }
    }
}
