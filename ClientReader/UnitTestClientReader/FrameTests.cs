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
            byte[] data = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x00 };
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

        [TestMethod()]
        public void packTestNullData()
        {
            Frame f = new Frame(0x00, 0x01, null);
            byte[] tmp = f.pack();
            Console.WriteLine(tmp.Length);
            Assert.AreEqual(4, tmp.Length);
            Assert.AreEqual(0x7D, tmp[(int)Frame.FIELDS.HEADER]);
            Assert.AreEqual(0x00, tmp[(int)Frame.FIELDS.LENGTH]);
            Assert.AreEqual(0x01, tmp[(int)Frame.FIELDS.CODE]);
            Assert.AreEqual(0x01, tmp[(int)Frame.FIELDS.DATA + f.Length]);
        }

        [TestMethod()]
        public void packTestData()
        {
            byte[] data = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x00 };
            Frame f = new Frame(0x08, 0x81, data);

            byte[] tmp = f.pack();
            Assert.AreEqual(12, tmp.Length);
            Assert.AreEqual(0x7D, tmp[(int)Frame.FIELDS.HEADER]);
            Assert.AreEqual(0x08, tmp[(int)Frame.FIELDS.LENGTH]);
            Assert.AreEqual(0x81, tmp[(int)Frame.FIELDS.CODE]);
            Assert.AreEqual(0x41, tmp[(int)Frame.FIELDS.DATA + 0]);
            Assert.AreEqual(0x44, tmp[(int)Frame.FIELDS.DATA + 3]);
            Assert.AreEqual(0x00, tmp[(int)Frame.FIELDS.DATA + 7]);
            Assert.AreEqual(0xC9, tmp[(int)Frame.FIELDS.DATA + f.Length]);
        }

        [TestMethod()]
        public void constructorFromPackTestNullData()
        {
            Frame f = new Frame(0x00, 0x01, null);
            byte[] tmp = f.pack();

            Frame f1 = new Frame(tmp);
            Assert.AreEqual(f, f1);
        }

        [TestMethod()]
        public void constructorFromPackTestData()
        {
            byte[] data = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x00 };
            Frame f = new Frame(0x08, 0x81, data);

            byte[] tmp = f.pack();

            Frame f1 = new Frame(tmp);
            
            Assert.AreEqual(f, f1);
        }

        [TestMethod()]
        public void equalsTest()
        {
            byte[] data1 = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x00 };
            Frame f1 = new Frame(0x08, 0x81, data1);

            byte[] data2 = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x00 };
            Frame f2 = new Frame(0x07, 0x81, data2);

            byte[] data3 = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x00 };
            Frame f3 = new Frame(0x07, 0x01, data3);

            byte[] data4 = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x00 };
            Frame f4 = new Frame(0x07, 0x01, data4);

            Frame fNull = new Frame(0x00, 0x01, null);

            Assert.AreNotEqual(f1, fNull);
            Assert.AreNotEqual(f1, f2);
            Assert.AreNotEqual(f2, f3);
            Assert.AreEqual(f3, f4);
        }

        [TestMethod()]
        public void isValidChecksumTest()
        {
            byte[] data1 = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x00 };
            Frame f1 = new Frame(0x08, 0x81, data1);

            byte []tmp = f1.pack();
            Frame f2 = new Frame(tmp);

            Assert.IsTrue(f2.isValidChecksum());

            tmp[(int)Frame.FIELDS.DATA + f1.Length] = 0x43;

            Frame f3 = new Frame(tmp);
            
            Assert.IsFalse(f3.isValidChecksum());
        }

        [TestMethod()]
        public void matchCodesTest()
        {
            byte[] data1 = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x00 };
            Frame f1 = new Frame(0x08, 0x01, data1);

            byte[] data2 = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x00 };
            Frame f2 = new Frame(0x07, 0x81, data2);

            Assert.IsTrue(Frame.matchCodes(f1,f2));

        }
    }
}