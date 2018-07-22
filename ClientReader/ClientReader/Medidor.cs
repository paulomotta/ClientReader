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

        protected void lerNumSerie()
        {
            Mensagem msg = Mensagem.createMensagemLerNumSerie();
            Mensagem response = canal.processRequest(msg);
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
