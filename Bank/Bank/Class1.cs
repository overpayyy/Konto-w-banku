namespace Bank
{
    public class Konto
    {
        private string klient;  // nazwa klienta
        private decimal bilans;  // aktualny stan środków na koncie
        private bool zablokowane = false; // stan konta

        // Konstruktor dwuargumentowy
        public Konto(string klient, decimal balansNaStart = 0)
        {
            this.klient = klient;
            this.bilans = balansNaStart;
        }

        // Właściwości typu read-only
        public string Nazwa => klient;
        public decimal Balans => bilans;
        public bool Zablokowane => zablokowane;

        // Metoda do wpłaty środków
        public void Wplata(decimal kwota)
        {
            if (zablokowane)
            {
                throw new InvalidOperationException("Konto jest zablokowane.");
            }

            else if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wpłaty musi być większa od zera.");
            }
            else
            {
                bilans += kwota;
            }
        }

        // Metoda do wypłaty środków
        public void Wyplata(decimal kwota)
        {
            if (zablokowane)
            {
                throw new InvalidOperationException("Konto jest zablokowane.");
            }
            else if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wypłaty musi być większa od zera.");
            }
            else if (kwota > bilans)
            {
                throw new InvalidOperationException("Niewystarczające środki na koncie.");
            }
            else
            {
                bilans -= kwota;
            }
        }

        // Metoda do blokowania konta
        public void BlokujKonto()
        {
            zablokowane = true;
        }

        // Metoda do odblokowywania konta
        public void OdblokujKonto()
        {
            zablokowane = false;
        }
    }
}