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

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_NiewystarczajaceSrodki_RzucaWyjatek()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wyplata(150);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wplata_KontoZablokowane_RzucaWyjatek()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.BlokujKonto();
            konto.Wplata(50);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_KontoZablokowane_RzucaWyjatek()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.BlokujKonto();
            konto.Wyplata(50);
        }

        [TestMethod]
        public void BlokujKonto_UstawiaZablokowaneNaTrue()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.BlokujKonto();
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void OdblokujKonto_UstawiaZablokowaneNaFalse()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.BlokujKonto();
            konto.OdblokujKonto();
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Wplata_NegatywnaKwota_RzucaWyjatek()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wplata(-50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Wyplata_NegatywnaKwota_RzucaWyjatek()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wyplata(-50);
        }
    }
}