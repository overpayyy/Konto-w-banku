using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class KontoPlusTests
    {
        [TestMethod]
        public void Konstruktor_InicjalizujePoprawnie()
        {
            string klient = "Jan Kowalski";
            decimal balansNaStart = 1000m;
            decimal limitDebetowy = 500m;

            KontoPlus kontoPlus = new KontoPlus(klient, balansNaStart, limitDebetowy);

            Assert.AreEqual(balansNaStart, kontoPlus.Balans - limitDebetowy);
            Assert.AreEqual(limitDebetowy, kontoPlus.JednorazowyLimitDebetowy);
        }

        [TestMethod]
        public void ZmienLimitDebetowy_UstawiaNowyLimit()
        {
            KontoPlus kontoPlus = new KontoPlus("Jan Kowalski", 1000m, 500m);
            decimal nowyLimit = 300m;

            kontoPlus.ZmienLimitDebetowy(nowyLimit);

            Assert.AreEqual(nowyLimit, kontoPlus.JednorazowyLimitDebetowy);
        }

        [TestMethod]
        public void Wplata_ZwiekszaBalans()
        {
            KontoPlus kontoPlus = new KontoPlus("Jan Kowalski", 1000m, 500m);
            decimal kwotaWplaty = 200m;

            kontoPlus.Wplata(kwotaWplaty);

            Assert.AreEqual(1200m, kontoPlus.Balans - kontoPlus.JednorazowyLimitDebetowy);
        }

        [TestMethod]
        public void Wyplata_ZmniejszaBalans()
        {
            KontoPlus kontoPlus = new KontoPlus("Jan Kowalski", 1000m, 500m);
            decimal kwotaWyplaty = 200m;

            kontoPlus.Wyplata(kwotaWyplaty);

            Assert.AreEqual(800m, kontoPlus.Balans - kontoPlus.JednorazowyLimitDebetowy);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_PrzekraczaLimitDebetowy_ThrowsInvalidOperationException()
        {
            KontoPlus kontoPlus = new KontoPlus("Jan Kowalski", 1000m, 500m);
            decimal kwotaWyplaty = 1600m;

            kontoPlus.Wyplata(kwotaWyplaty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_ZablokowaneKonto_ThrowsInvalidOperationException()
        {
            KontoPlus kontoPlus = new KontoPlus("Jan Kowalski", 1000m, 500m);
            kontoPlus.Wyplata(1500m); 

            kontoPlus.Wyplata(100m);
        }

        [TestMethod]
        public void Wplata_OdblokowujeKonto()
        {
            KontoPlus kontoPlus = new KontoPlus("Jan Kowalski", 1000m, 500m);
            kontoPlus.Wyplata(1000m); 

            kontoPlus.Wplata(600m); 

            Assert.IsFalse(kontoPlus.Balans < 0);
        }
    }
}
