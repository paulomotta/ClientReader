using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    public class Leitura
    {
        private int indiceRegistro;
        private string numSerie;
        private string dataHora;
        private float valor;
        private bool faltante;
        private string dataHoraLeitura;

        public int IndiceRegistro
        {
            get
            {
                return indiceRegistro;
            }
        }

        public string NumSerie
        {
            get
            {
                return numSerie;
            }
        }

        public string DataHora
        {
            get
            {
                return dataHora;
            }
        }

        public float Valor
        {
            get
            {
                return valor;
            }
        }

        public bool Faltante
        {
            get
            {
                return faltante;
            }
        }

        public string DataHoraLeitura
        {
            get
            {
                return dataHoraLeitura;
            }
        }

        public Leitura(int indiceRegistro, string numSerie, string dataHora, float valor)
        {
            this.indiceRegistro = indiceRegistro;
            this.numSerie = numSerie;
            this.dataHora = dataHora;
            this.valor = valor;
            this.faltante = false;
            this.dataHoraLeitura = (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public Leitura(int indiceRegistro, string numSerie)
        {
            this.indiceRegistro = indiceRegistro;
            this.numSerie = numSerie;
            this.dataHora = null;
            this.valor = 0;
            this.faltante = true;
            this.dataHoraLeitura = (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
