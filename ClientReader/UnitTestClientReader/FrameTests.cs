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
    public class FrameTests
    {
        [TestMethod()]
        public void createChecksumNullDataTest()
        {
            Frame f = new Frame(0x00, 0x01, null);
            byte expected = 0x01;
            Assert.AreEqual(expected, f.createChecksum());

        }

        [TestMethod()]
        public void createChecksumDataTest()
        {
            byte []data = {0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x00};
            Frame f = new Frame(0x08, 0x81, data);
            byte expected = 0xC9;
            Assert.AreEqual(expected, f.createChecksum());

        }

        [TestMethod()]
        public void xorDataTestNullData()
        {
            Frame f = new Frame(0x00, 0x01, null);
            byte expected = 0x00;
            Assert.AreEqual(expected, f.xorData());
        }

        [TestMethod()]
        public void xorDataTestData()
        {
            byte[] data = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x00 };
            Frame f = new Frame(0x08, 0x81, data);
            byte expected = 0x40;
            Assert.AreEqual(expected, f.xorData());
        }
    }
}