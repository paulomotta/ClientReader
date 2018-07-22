using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ClientReader.Tests
{
    [TestClass()]
    public class CanalSocketTests
    {
        EchoServer server;
        Thread t;

        [TestInitialize()]
        public void Initialize()
        {
            server = new EchoServer(20015);
            t = new Thread(new ThreadStart(server.init));
            t.Name = "Listener";
            t.Start();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            server.stop();
        }

        [TestMethod()]
        public void connectAndDisconnectTest()
        {
            Canal c = new CanalSocket("localhost", 20015);
            bool result = c.connect();
            Assert.IsTrue(result);
            result = false;
            result = c.disconnect();
            Assert.IsTrue(result);   
        }

        [TestMethod()]
        public void concreteSendReceiveTest()
        {
            Canal c = new CanalSocket("localhost", 20015);
            bool result = c.connect();
            Assert.IsTrue(result);

            Mensagem src = Mensagem.createMensagemDeErro();
            bool errorCode = c.concreteSend(src);

            Assert.IsTrue(errorCode);

            Mensagem msg = c.concreteReceive();

            Assert.IsNotNull(msg);

            result = false;
            result = c.disconnect();
            Assert.IsTrue(result);
        }
    }
}