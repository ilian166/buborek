using System;
using System.Collections.Generic;
using System.Linq;

namespace ElatkozottBurok
{
    public class Automata
    {



    
        public int KeszpenzKassza { get; private set; } = 0;
        public List<Nassolnivalo> Keszlet { get; private set; } = new List<Nassolnivalo>();
        public bool Elakadva { get; private set; } = false;

        private static Random rand = new Random();

        public void Feltolt(List<Nassolnivalo> ujElemek)
        {
            if (ujElemek != null)
            {
                Keszlet.AddRange(ujElemek);
            }
        }

        public Nassolnivalo Vasarlas(string termekNev, Fejleszto vasarlo)
        {
            if (Elakadva)
            {
                Console.WriteLine($"[AUTOMATA] Elakadás van! {vasarlo.Nev} stressze megnőtt.");
                vasarlo.StresszSzint += 15;
                return null;
            }

            var termek = Keszlet.FirstOrDefault(t => t.Nev.Equals(termekNev, StringComparison.OrdinalIgnoreCase));
            if (termek == null)
            {
                Console.WriteLine($"[AUTOMATA] A termék kifogyott: {termekNev}");
                return null;
            }

            if (vasarlo.Penz < termek.Ar)
            {
                Console.WriteLine($"[AUTOMATA] {vasarlo.Nev}-nek nincs elég pénze erre: {termek.Nev} ({termek.Ar} Ft)");
                return null;
            }

            int esely = rand.Next(1, 101);
            if (esely < 15)
            {
                Elakadva = true;
                vasarlo.Penz -= termek.Ar;
                KeszpenzKassza += termek.Ar;
                vasarlo.StresszSzint += 30;
                Console.WriteLine($"[PECH] Az automata elakadt vásárlás közben! A pénzt levonta, de a terméket nem adta ki.");
                return null;
            }

            vasarlo.Penz -= termek.Ar;
            KeszpenzKassza += termek.Ar;
            Keszlet.Remove(termek);
            return termek;
        }

        public void JavitasRugassal()
        {
            if (!Elakadva) return;

            int esely = rand.Next(1, 101);
            if (esely <= 50)
            {
                Elakadva = false;
                Console.WriteLine("[AUTOMATA] Sikerült megjavítani a gépet egy határozott rúgással!");
            }
            else
            {
                Console.WriteLine("[RIASZTÓ] Riasztó szólal meg! A rúgás nem segített, a helyzet romlott.");
            }
        }
    }

}
