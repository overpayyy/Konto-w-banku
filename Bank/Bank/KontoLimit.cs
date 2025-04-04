namespace Bank
{
    public class KontoLimit
    {
        private Konto konto;
        private decimal jednorazowyLimitDebetowy;
        private bool debetWykorzystany = false;

        // Konstruktor dwuargumentowy z limitem debetowym
        public KontoLimit(string klient, decimal balansNaStart = 0, decimal limitDebetowy = 0)
        {
            konto = new Konto(klient, balansNaStart);
            jednorazowyLimitDebetowy = limitDebetowy;
        }


        // Właściwość do odczytu limitu debetowego
        public decimal JednorazowyLimitDebetowy => jednorazowyLimitDebetowy;

        // Metoda do zmiany limitu debetowego
        public void ZmienLimitDebetowy(decimal nowyLimit)
        {
            if (nowyLimit < 0)
            {
                throw new ArgumentException("Limit debetowy musi być większy lub równy zero.");
            }
            jednorazowyLimitDebetowy = nowyLimit;
        }

        // Właściwość do odczytu balansu
        public decimal Balans => konto.Balans + (debetWykorzystany ? 0 : jednorazowyLimitDebetowy);

        // Metoda do wpłaty środków
        public void Wplata(decimal kwota)
        {
            konto.Wplata(kwota);
            if (konto.Balans >= 0)
            {
                debetWykorzystany = false;
                konto.OdblokujKonto();
            }
        }

        // Metoda do wypłaty środków
        public void Wyplata(decimal kwota)
        {
            if (konto.Zablokowane)
            {
                throw new InvalidOperationException("Konto jest zablokowane.");
            }
            else if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wypłaty musi być większa od zera.");
            }
            else if (kwota > (Balans + JednorazowyLimitDebetowy))
            {
                throw new InvalidOperationException("Niewystarczające środki na koncie.");
            }
            else
            {
                konto.Wyplata(kwota);
                if (konto.Balans < 0)
                {
                    konto.BlokujKonto();
                }
            }
        }
    }
}
