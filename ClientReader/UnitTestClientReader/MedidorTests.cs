using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientReader.Tests
{
    [TestClass()]
    public class MedidorTests
    {
        [TestMethod()]
        public void lerNumSerieTest()
        {
            CanalTeste ct = new CanalTeste();
            Medidor m = new Medidor(ct);

            Assert.AreEqual("ABCDEFG", m.lerNumSerie());
                
        }

        [TestMethod()]
        public void lerStatusTest()
        {
            CanalTeste ct = new CanalTeste();
            Medidor m = new Medidor(ct);
            UInt16 []reg = m.lerRegistroStatus();
            Assert.AreEqual(300, reg[0]);
            Assert.AreEqual(600, reg[1]);
        }

        [TestMethod()]
        public void definirIndiceLeituraTest()
        {
            CanalTeste ct = new CanalTeste();
            Medidor m = new Medidor(ct);

            Assert.AreEqual(0, m.definirIndiceLeitura(127));

        }

        [TestMethod()]
        public void lerDataHoraRegistroAtualTest()
        {
            CanalTeste ct = new CanalTeste();
            Medidor m = new Medidor(ct);

            Assert.AreEqual("2014-01-23 17:25:10", m.lerDataHoraRegistroAtual());

        }

        [TestMethod()]
        public void lerValorEnergiaRegistroAtualTest()
        {
            CanalTeste ct = new CanalTeste();
            Medidor m = new Medidor(ct);

            Assert.AreEqual(1234.015f, m.lerValorEnergiaRegistroAtual());

        }

    }
}