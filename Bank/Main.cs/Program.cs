using System;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tworzenie konta zwykłego
            Konto konto1 = new Konto("Jan Kowalski", 1000);
            Console.WriteLine($"Konto 1: {konto1.Nazwa}, Bilans: {konto1.Balans} zł");

            // Wpłata na konto 1
            konto1.Wplata(500);
            Console.WriteLine($"Po wpłacie 500 zł, bilans Konto 1: {konto1.Balans} zł");

            // Próba wypłaty z konta 1
            try
            {
                konto1.Wyplata(300);
                Console.WriteLine($"Po wypłacie 300 zł, bilans Konto 1: {konto1.Balans} zł");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd wypłaty: {ex.Message}");
            }

            // Tworzenie konta z limitem debetowym (KontoPlus)
            KontoPlus kontoPlus = new KontoPlus("Anna Nowak", 500, 1000);
            Console.WriteLine($"Konto Plus: {kontoPlus.Nazwa}, Bilans: {kontoPlus.Balans} zł, Limit debetowy: {kontoPlus.JednorazowyLimitDebetowy} zł");

            // Wpłata na konto Plus
            kontoPlus.Wplata(200);
            Console.WriteLine($"Po wpłacie 200 zł, bilans Konto Plus: {kontoPlus.Balans} zł");

            // Wypłata, przekraczająca bilans ale w ramach limitu debetowego
            try
            {
                kontoPlus.Wyplata(1200); // Próba wypłaty z debetem
                Console.WriteLine($"Po wypłacie 1200 zł, bilans Konto Plus: {kontoPlus.Balans} zł");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd wypłaty: {ex.Message}");
            }

            // Tworzenie konta z limitem debetowym (KontoLimit)
            KontoLimit kontoLimit = new KontoLimit("Marek Zawisza", 200, 500);
            Console.WriteLine($"Konto Limit: {kontoLimit.Balans} zł, Limit debetowy: {kontoLimit.JednorazowyLimitDebetowy} zł");

            // Wpłata na KontoLimit
            kontoLimit.Wplata(150);
            Console.WriteLine($"Po wpłacie 150 zł, bilans Konto Limit: {kontoLimit.Balans} zł");

            // Wypłata przekraczająca bilans, ale w ramach limitu debetowego
            try
            {
                kontoLimit.Wyplata(600); // Wypłata przekraczająca bilans z debetem
                Console.WriteLine($"Po wypłacie 600 zł, bilans Konto Limit: {kontoLimit.Balans} zł");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd wypłaty: {ex.Message}");
            }

            // Blokada konta
            konto1.BlokujKonto();
            try
            {
                konto1.Wyplata(200); // Próba wypłaty z zablokowanego konta
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd wypłaty z zablokowanego konta: {ex.Message}");
            }
        }
    }
}
