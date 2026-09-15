using System;

namespace ElatkozottBurok
{
    public class Fejleszto
    {
    
        public string Nev { get; set; }
        public Munkakor Munkakor { get; set; }

        private int penz;
        public int Penz
        {
            get => penz;
            set => penz = Math.Max(0, value);
        }

        private int koffeinszint;
        public int Koffeinszint
        {
            get => koffeinszint;
            set
            {
                koffeinszint = Math.Clamp(value, 0, 100);
                if (koffeinszint >= 100)
                {
                    Kiegve = true;
                }
            }
        }

        private int stresszSzint;
        public int StresszSzint
        {
            get => stresszSzint;
            set
            {
                stresszSzint = Math.Clamp(value, 0, 100);
                if (stresszSzint >= 100)
                {
                    Kiegve = true;
                }
            }
        }

        public bool Kiegve { get; private set; } = false;
        public string KedvencSnack { get; set; }

        public Fejleszto(string nev, Munkakor munkakor, int penz, int koffeinszint, int stresszSzint, string kedvencSnack)
        {
            Nev = nev;
            Munkakor = munkakor;
            Penz = penz;
            Koffeinszint = koffeinszint;
            StresszSzint = stresszSzint;
            KedvencSnack = kedvencSnack;
        }

        public void Dolgozik()
        {
            if (Kiegve)
            {
                Console.WriteLine($"[HIBA] {Nev} kiégett / túladagolta magát, ma már nem tud dolgozni!");
                return;
            }

            switch (Munkakor)
            {
                case Munkakor.Junior:
                    Koffeinszint -= 25;
                    StresszSzint += 20;
                    break;
                case Munkakor.Senior:
                    Koffeinszint -= 15;
                    StresszSzint += 10;
                    break;
                case Munkakor.DevOpsVarazslo:
                    Koffeinszint -= 10;
                    StresszSzint += 25;
                    break;
            }

            if (Koffeinszint < 15)
            {
                Console.WriteLine($"[FIGYELEM] {Nev} agya lefagyott, koffeinre van szüksége!");
            }
        }

        public void Fogyaszt(Nassolnivalo elem)
        {
            if (elem == null) return;

            int tenylegesStresszOldas = elem.StresszOldas;
            int extraKoffein = 0;

            if (elem.Nev.Equals(KedvencSnack, StringComparison.OrdinalIgnoreCase))
            {
                tenylegesStresszOldas *= 2;
                extraKoffein = 5;
            }

            Koffeinszint += elem.KoffeinLoket + extraKoffein;
            StresszSzint -= tenylegesStresszOldas;
        }
    }
}
