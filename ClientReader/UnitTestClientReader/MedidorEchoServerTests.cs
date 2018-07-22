using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ClientReader.Tests
{

    [TestClass()]
    public class MedidorEchoServerTests
    {
        EchoServer server;
        Thread t;
        Canal c;
        Medidor m;

        [TestInitialize()]
        public void Initialize()
        {
            Debug.WriteLine("Init");
            server = new EchoServer(20015);
            t = new Thread(new ThreadStart(server.init));
            t.Name = "Listener";
            t.Start();
            Debug.WriteLine("Listening");
            c = new CanalSocket("localhost", 20015);
            Debug.WriteLine("Canal criado");
            m = new Medidor(c);
            Debug.WriteLine("Medidor criado");
            m.connect();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            m.disconnect();
            server.stop();
        }

        [TestMethod()]
        public void lerNumSerieTest()
        {
            Assert.AreEqual("ABCDEFG", m.lerNumSerie());
        }

        [TestMethod()]
        public void lerStatusTest()
        {
            UInt16 []reg = m.lerRegistroStatus();
            Assert.AreEqual(300, reg[0]);
            Assert.AreEqual(600, reg[1]);
        }

        [TestMethod()]
        public void definirIndiceLeituraTest()
        {
            Assert.AreEqual(0, m.definirIndiceLeitura(127));

        }

        [TestMethod()]
        public void lerDataHoraRegistroAtualTest()
        {
            Assert.AreEqual("2014-01-23 17:25:10", m.lerDataHoraRegistroAtual());

        }

        [TestMethod()]
        public void lerValorEnergiaRegistroAtualTest()
        {
            Assert.AreEqual(10.0f, m.lerValorEnergiaRegistroAtual());
        }

    }
}