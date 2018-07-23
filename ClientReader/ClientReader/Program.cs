using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            StreamReader file = new StreamReader(args[0]);
            StreamWriter saida = new StreamWriter("leitura.txt");
            StreamWriter faltantes = new StreamWriter("missing.txt");
            NumberFormatInfo nfi = new CultureInfo("pt-BR", false).NumberFormat;

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("#")) continue; //ignorar linhas comentadas

                string[] tmp = line.Split(' ');
                Pedido pedido = new Pedido(tmp[0], int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]));
                //Canal c = new CanalSocket(pedido.Ip, pedido.Porta);
                Canal c = new CanalTeste();
                Medidor medidor = new Medidor(c);
                string numSerie = medidor.lerNumSerie();
                saida.WriteLine(numSerie);  
                UInt16 []registros = medidor.lerRegistroStatus();

                for (int i = pedido.IndiceInicial; i <= pedido.IndiceFinal; i++)
                {
                    if (i < registros[0] || i > registros[1]) //indice inicial anterior ao que existe no medidor
                    {
                        Leitura leitura = new Leitura(i,numSerie);
                        pedido.Leituras.Add(leitura);
                        faltantes.WriteLine("{0};{1};{2}", leitura.NumSerie,
                                            leitura.IndiceRegistro,
                                            leitura.DataHoraLeitura);
                        faltantes.Flush();
                    }
                    else
                    {
                        if(medidor.definirIndiceLeitura((UInt16)i) ==0)
                        {
                            string dataHora = medidor.lerDataHoraRegistroAtual();
                            float valor = medidor.lerValorEnergiaRegistroAtual();

                            Leitura leitura = new Leitura(i, numSerie, dataHora, valor);
                            pedido.Leituras.Add(leitura);
                            saida.WriteLine("{0};{1};{2}",leitura.IndiceRegistro,
                                            leitura.DataHora,
                                            (Math.Round(leitura.Valor,2, MidpointRounding.ToEven)).ToString("0.00", nfi));
                            saida.Flush();
                        }
                        else
                        {
                            Leitura leitura = new Leitura(i, numSerie);
                            pedido.Leituras.Add(leitura);
                            faltantes.WriteLine("{0};{1};{2}", leitura.NumSerie,
                                            leitura.IndiceRegistro,
                                            leitura.DataHoraLeitura);
                            faltantes.Flush();
                        }
                        
                    }
                }
            }
            saida.Close();
            faltantes.Close();
            file.Close();
        }
    }   
}
