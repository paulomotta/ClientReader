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
    public class MensagemTests
    {
        [TestMethod()]
        public void createMensagemLerNumSerieTest()
        {
            Mensagem m = Mensagem.createMensagemLerNumSerie();
            Assert.AreEqual(0x01,m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.LerNumSerie, m.Frame.Code);
        }

        [TestMethod()]
        public void createMensagemLerStatusTest()
        {
            Mensagem m = Mensagem.createMensagemLerStatus();
            Assert.AreEqual(0x02, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.LerStatus, m.Frame.Code);
        }

        [TestMethod()]
        public void createMensagemDefinirRegistroTest()
        {
            Mensagem m = Mensagem.createMensagemDefinirRegistro(380);
            Assert.AreEqual(0x7C, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.DefinirRegistro, m.Frame.Code);
            Assert.AreEqual(2, m.Frame.Data.Length);
            Assert.AreEqual(0x7C, m.Frame.Data[0]);
            Assert.AreEqual(0x01, m.Frame.Data[1]);
        }
    }
}