using System;

namespace ElatkozottBurok
{

    public class Nassolnivalo
    {
        private string nev;
        private int koffeinLoket;
        private int stresszOldas;
        private int ar;
        public string Nev
        {
            get => nev;
            set => nev = string.IsNullOrWhiteSpace(value) ? "Ismeretlen nassolnivaló" : value;
        }
        public int KoffeinLoket
        {
            get => koffeinLoket;
            set => koffeinLoket = Math.Clamp(value, 0, 50);
        }
        public int StresszOldas
        {
            get => stresszOldas;
            set => stresszOldas = Math.Clamp(value, 0, 30);
        }
        public int Ar
        {
            get => ar;
            set => ar = Math.Max(100, value);
        }
        public Nassolnivalo(string nev, int koffeinLoket, int stresszOldas, int ar)
        {
            Nev = nev;
            KoffeinLoket = koffeinLoket;
            StresszOldas = stresszOldas;
            Ar = ar;
        }
    }
}


       