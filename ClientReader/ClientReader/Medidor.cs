using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        }

        public bool connect()
        {
            return canal.connect();
        }

        public bool disconnect()
        {
            return canal.disconnect();
        }

        public string lerNumSerie()
        {
            Mensagem msg = Mensagem.createMensagemLerNumSerie();
            Debug.WriteLine(msg);
            Mensagem response = canal.processRequest(msg);
            Debug.WriteLine(response);
            string numSerie = Mensagem.ByteArrayToString(response.Frame.Data);
            Debug.WriteLine(numSerie);
            return numSerie;
        }
        public UInt16[] lerRegistroStatus()
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

            UInt16[] reg = { Mensagem.ByteArrayToUInt16(antigo), Mensagem.ByteArrayToUInt16(novo) }; 
                
            Console.WriteLine(reg[0] + " " + reg[1]);
            return reg;
        }
        public byte definirIndiceLeitura(UInt16 indice)
        {
            Mensagem msg = Mensagem.createMensagemDefinirRegistro(indice);
            Console.WriteLine(msg);
            Mensagem response = canal.processRequest(msg);
            Console.WriteLine(response);
            return response.Frame.Data[0];

        }
        public string lerDataHoraRegistroAtual()
        {
            Mensagem msg = Mensagem.createMensagemLerDataHoraRegistroAtual();
            Console.WriteLine(msg);
            Mensagem response = canal.processRequest(msg);
            Console.WriteLine(response);
            return Mensagem.ByteArrayToDateTimeString(response.Frame.Data);
        }
        public float lerValorEnergiaRegistroAtual()
        {
            Mensagem msg = Mensagem.createMensagemLerValorRegistroAtual();
            Console.WriteLine(msg);
            Mensagem response = canal.processRequest(msg);
            Console.WriteLine(response);
            return Mensagem.IEEE754ByteArrayToFloat(response.Frame.Data);
        }
        
    }
}
