using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public class Pedido
    {
        private string ip;
        private int porta;
        private int indiceInicial;
        private int indiceFinal;

        public string Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public int Porta
        {
            get
            {
                return porta;
            }

            set
            {
                porta = value;
            }
        }

        public int IndiceInicial
        {
            get
            {
                return indiceInicial;
            }

            set
            {
                indiceInicial = value;
            }
        }

        public int IndiceFinal
        {
            get
            {
                return indiceFinal;
            }

            set
            {
                indiceFinal = value;
            }
        }

        public Pedido(string ip, int porta, int indiceInicial, int indiceFinal)
        {
            this.ip = ip;
            this.porta = porta;
            this.indiceInicial = indiceInicial;
            this.indiceFinal = indiceFinal;
        }

        public override string ToString()
        {
            return "["+ip+" "+porta+" " + indiceInicial + " " +indiceFinal+"]";
        }
    }
}
