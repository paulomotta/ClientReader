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
        public void createMensagemRespLerNumSerieTest()
        {
            Mensagem m = Mensagem.createMensagemRespLerNumSerie("ABC\0");
            Assert.AreEqual(0xC5, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.RespLerNumSerie, m.Frame.Code);
            Assert.AreEqual("ABC",Mensagem.ByteArrayToString(m.Frame.Data));
        }

        [TestMethod()]
        public void createMensagemLerStatusTest()
        {
            Mensagem m = Mensagem.createMensagemLerStatus();
            Assert.AreEqual(0x02, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.LerStatus, m.Frame.Code);
        }

        [TestMethod()]
        public void createMensagemRespLerStatusTest()
        {
            Mensagem m = Mensagem.createMensagemRespLerStatus(127,457);
            Assert.AreEqual(0x31, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.RespLerStatus, m.Frame.Code);

            byte[] data = m.Frame.Data;
            byte[] antigo = new byte[2];
            byte[] novo = new byte[2];

            antigo[0] = data[0];
            antigo[1] = data[1];
            novo[0] = data[2];
            novo[1] = data[3];

            UInt16 regAntigo = Mensagem.ByteArrayToUInt16(antigo);
            UInt16 regNovo = Mensagem.ByteArrayToUInt16(novo);
            Assert.AreEqual(127, regAntigo);
            Assert.AreEqual(457, regNovo);
        }

        [TestMethod()]
        public void createMensagemDefinirRegistroTest()
        {
            Mensagem m = Mensagem.createMensagemDefinirRegistro(380);
            Assert.AreEqual(0x7C, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.DefinirRegistro, m.Frame.Code);
            Assert.AreEqual(2, m.Frame.Data.Length);
            Assert.AreEqual(0x01, m.Frame.Data[0]);
            Assert.AreEqual(0x7C, m.Frame.Data[1]);
        }

        [TestMethod()]
        public void createMensagemLerDataHoraRegistroAtualTest()
        {
            Mensagem m = Mensagem.createMensagemLerDataHoraRegistroAtual();
            Assert.AreEqual(0x04, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.LerDataHora, m.Frame.Code);
        }

        [TestMethod()]
        public void createMensagemLerValorRegistroAtualTest()
        {
            Mensagem m = Mensagem.createMensagemLerValorRegistroAtual();
            Assert.AreEqual(0x05, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.LerValor, m.Frame.Code);
        }

        [TestMethod()]
        public void createMensagemDeErro()
        {
            Mensagem m = Mensagem.createMensagemDeErro();
            Assert.AreEqual(0xFF, m.Frame.Checksum);
            Assert.AreEqual((byte)Frame.CODE.Erro, m.Frame.Code);
        }

        [TestMethod()]
        public void reverseByteArrayTest()
        {
            byte[] data = { 0x07, 0x11, 0x17, 0x29 };
            byte[] result = Mensagem.reverseByteArray(data);
            Assert.AreEqual(0x29, result[0]);
            Assert.AreEqual(0x17, result[1]);
            Assert.AreEqual(0x11, result[2]);
            Assert.AreEqual(0x07, result[3]);

        }

        [TestMethod()]
        public void UInt16ToByteArrayAndBackTest()
        {
            UInt16 num = 380;
            byte[] data = Mensagem.UInt16ToByteArray(num);
            Assert.AreEqual(0x01, data[0]);
            Assert.AreEqual(0x7C, data[1]);

            UInt16 result = Mensagem.ByteArrayToUInt16(data);
            Assert.AreEqual(num, result);
        }

        [TestMethod()]
        public void FloatIEEE754ToByteArrayAndBackTest()
        {
            float num = 10.0f;
            byte[] data = Mensagem.FloatToIEEE754ByteArray(num);
            Assert.AreEqual(0x41, data[0]);
            Assert.AreEqual(0x20, data[1]);
            Assert.AreEqual(0x00, data[2]);
            Assert.AreEqual(0x00, data[3]);

            float result = Mensagem.IEEE754ByteArrayToFloat(data);
            Assert.AreEqual(num, result);
        }

        [TestMethod()]
        public void ByteArrayToDateTimeStringTest()
        {
            //7D E1 BC 59 2B – contem data e hora 2014-01-23 17:25:10
            byte[] data = { 0x7D, 0xE1, 0xBC, 0x59, 0x2B };

            string result = Mensagem.ByteArrayToDateTimeString(data);
            Assert.AreEqual("2014-01-23 17:25:10",result);
        }
    }
}