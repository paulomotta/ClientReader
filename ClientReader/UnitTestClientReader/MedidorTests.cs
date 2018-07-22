﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        
    }
}