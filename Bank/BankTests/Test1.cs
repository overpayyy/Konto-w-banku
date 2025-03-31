using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class KontoTests
    {
        [TestMethod]
        public void Wplata_DodajeKwoteDoBalansu()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wplata(50);
            Assert.AreEqual(150, konto.Balans);
        }

        [TestMethod]
        public void Wyplata_OdejmowanieKwotyOdBalansu()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wyplata(50);
            Assert.AreEqual(50, konto.Balans);
        }
    }
}