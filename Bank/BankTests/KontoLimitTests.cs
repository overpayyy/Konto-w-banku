using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class KontoLimitTests
    {
        [TestMethod]
        public void Konstruktor_InicjalizujePoprawnie()
        {
            string klient = "Jan Kowalski";
            decimal balansNaStart = 1000m;
            decimal limitDebetowy = 500m;

            KontoLimit kontoLimit = new KontoLimit(klient, balansNaStart, limitDebetowy);

            Assert.AreEqual(balansNaStart, kontoLimit.Balans - limitDebetowy);
            Assert.AreEqual(limitDebetowy, kontoLimit.JednorazowyLimitDebetowy);
        }

        [TestMethod]
        public void ZmienLimitDebetowy_UstawiaNowyLimit()
        {
            KontoLimit kontoLimit = new KontoLimit("Jan Kowalski", 1000m, 500m);
            decimal nowyLimit = 300m;

            kontoLimit.ZmienLimitDebetowy(nowyLimit);

            Assert.AreEqual(nowyLimit, kontoLimit.JednorazowyLimitDebetowy);
        }

        [TestMethod]
        public void Wplata_ZwiekszaBalans()
        {
            KontoLimit kontoLimit = new KontoLimit("Jan Kowalski", 1000m, 500m);
            decimal kwotaWplaty = 200m;

            kontoLimit.Wplata(kwotaWplaty);

            Assert.AreEqual(1200m, kontoLimit.Balans - kontoLimit.JednorazowyLimitDebetowy);
        }

        [TestMethod]
        public void Wyplata_ZmniejszaBalans()
        {
            KontoLimit kontoLimit = new KontoLimit("Jan Kowalski", 1000m, 500m);
            decimal kwotaWyplaty = 200m;

            kontoLimit.Wyplata(kwotaWyplaty);

            Assert.AreEqual(800m, kontoLimit.Balans - kontoLimit.JednorazowyLimitDebetowy);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_PrzekraczaLimitDebetowy_ThrowsInvalidOperationException()
        {
            KontoLimit kontoLimit = new KontoLimit("Jan Kowalski", 1000m, 500m);
            decimal kwotaWyplaty = 1600m;

            kontoLimit.Wyplata(kwotaWyplaty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_ZablokowaneKonto_ThrowsInvalidOperationException()
        {
            KontoLimit kontoLimit = new KontoLimit("Jan Kowalski", 1000m, 500m);
            kontoLimit.Wyplata(1500m);

            kontoLimit.Wyplata(100m);
        }

        [TestMethod]
        public void Wplata_OdblokowujeKonto()
        {
            KontoLimit kontoLimit = new KontoLimit("Jan Kowalski", 1000m, 500m);
            kontoLimit.Wyplata(1500m);

            kontoLimit.Wplata(600m); 

            Assert.IsFalse(kontoLimit.Balans < 0);
        }
    }
}