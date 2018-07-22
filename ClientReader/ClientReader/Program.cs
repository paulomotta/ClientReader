using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Por favor indique o nome do arquivo de pedidos.");
                System.Console.WriteLine("Uso: ClientReader <nome_do_arquivo.txt>");
                return;
            }

            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(args[0]);
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("#")) continue; //ignorar linhas comentadas

                string[] tmp = line.Split(' ');
                Pedido pedido = new Pedido(tmp[0], int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]));
                //Canal c = new CanalSocket(pedido.Ip, pedido.Porta);
                Canal c = new CanalTeste();
                Medidor medidor = new Medidor(c);
                string numSerie = medidor.lerNumSerie();
                UInt16 []registros = medidor.lerRegistroStatus();

                for (int i = pedido.IndiceInicial; i <= pedido.IndiceFinal; i++)
                {
                    if (i < registros[0] || i > registros[1]) //indice inicial anterior ao que existe no medidor
                    {
                        Leitura leitura = new Leitura(i,numSerie);
                        pedido.Leituras.Add(leitura);
                    }
                    else
                    {
                        if(medidor.definirIndiceLeitura((UInt16)i) ==0)
                        {
                            string dataHora = medidor.lerDataHoraRegistroAtual();
                            float valor = medidor.lerValorEnergiaRegistroAtual();

                            Leitura leitura = new Leitura(i, numSerie, dataHora, valor);
                            pedido.Leituras.Add(leitura);
                        }
                        else
                        {
                            Leitura leitura = new Leitura(i, numSerie);
                            pedido.Leituras.Add(leitura);
                            //TODO gerar um log de registro faltante, mas dentro do intervalo
                        }
                        
                    }
                }
                
            }

            file.Close();
        }
    }   
}
