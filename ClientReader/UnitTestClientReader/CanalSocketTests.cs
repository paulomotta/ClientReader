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
        
        [TestMethod()]
        public void connectTest()
        {
            EchoServer server = new EchoServer(20015);
            Thread t = new Thread(new ThreadStart(server.init));
            t.Name = "Listener";
            t.Start();
            Canal c = new CanalSocket("localhost", 20015);
            bool result = c.connect();
            Assert.IsTrue(result);
            result = false;
            result = c.disconnect();
            Assert.IsTrue(result);
            server.stop();
            
        }

        [TestMethod()]
        public void disconnectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void concreteSendTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void concreteReceiveTest()
        {
            Assert.Fail();
        }
    }
}