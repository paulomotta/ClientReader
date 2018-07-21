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
                Canal c = new CanalSocket(pedido.Ip, pedido.Porta);
                Medidor medidor = new Medidor(c);

                for (int i = pedido.IndiceInicial; i <= pedido.IndiceFinal; i++)
                {
                        
                    medidor.executarLeitura();
                }
                
            }

            file.Close();
        }
    }   
}
