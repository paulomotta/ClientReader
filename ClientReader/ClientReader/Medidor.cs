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
        public void lerRegistroStatus()
        {

        }
        public void definirIndiceLeitura()
        {

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
