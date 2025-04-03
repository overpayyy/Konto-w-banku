namespace Bank
{
    public class KontoPlus : Konto
    {
        private decimal jednorazowyLimitDebetowy;
        private bool debetWykorzystany = false;

        // Konstruktor dwuargumentowy z limitem debetowym
        public KontoPlus(string klient, decimal balansNaStart = 0, decimal limitDebetowy = 0)
            : base(klient, balansNaStart)
        {
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

        // Nadpisana właściwość Balans
        public new decimal Balans => base.Balans + (debetWykorzystany ? 0 : jednorazowyLimitDebetowy);

        // Nadpisana metoda do wpłaty środków
        public new void Wplata(decimal kwota)
        {
            base.Wplata(kwota);
            if (base.Balans >= 0)
            {
                debetWykorzystany = false;
                OdblokujKonto();
            }
        }

        // Nadpisana metoda do wypłaty środków
        public new void Wyplata(decimal kwota)
        {
            if (Zablokowane)
            {
                throw new InvalidOperationException("Konto jest zablokowane.");
            }

            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wypłaty musi być większa od zera.");
            }

            if (kwota > base.Balans + jednorazowyLimitDebetowy)
            {
                throw new InvalidOperationException("Niewystarczające środki na koncie.");
            }

            if (kwota > base.Balans)
            {
                base.Wyplata(base.Balans);
                debetWykorzystany = true;
                BlokujKonto();
            }
            else
            {
                base.Wyplata(kwota);
            }
        }
    }
}