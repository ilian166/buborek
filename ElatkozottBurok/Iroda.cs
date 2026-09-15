using System;
using System.Collections.Generic;
using System.Linq;

namespace ElatkozottBurok
{
    public class Iroda
    {


        public List<Fejleszto> Fejlesztok { get; set; } = new List<Fejleszto>();
        public Automata AutomataGep { get; set; } = new Automata();

        public void MunkanapSzimulacio(int orakSzama)
        {
            for (int ora = 1; ora <= orakSzama; ora++)
            {
                Console.WriteLine($"\n--- {ora}. ÓRA ---");
                foreach (var f in Fejlesztok)
                {
                    f.Dolgozik();

                    if (f.Koffeinszint < 20 || f.StresszSzint > 70)
                    {
                        Console.WriteLine($"[IRODA] {f.Nev} állapota kritikus (Koffein: {f.Koffeinszint}, Stressz: {f.StresszSzint}). Irány az automata!");

                        var termek = AutomataGep.Keszlet.FirstOrDefault(t => t.Nev.Equals(f.KedvencSnack, StringComparison.OrdinalIgnoreCase));
                        if (termek == null && AutomataGep.Keszlet.Count > 0)
                        {
                            termek = AutomataGep.Keszlet.OrderBy(t => t.Ar).First(); 
                        }

                        if (termek != null)
                        {
                            var megvasarolt = AutomataGep.Vasarlas(termek.Nev, f);
                            if (megvasarolt != null)
                            {
                                f.Fogyaszt(megvasarolt);
                                Console.WriteLine($"[IRODA] {f.Nev} sikeresen elfogyasztotta: {megvasarolt.Nev}.");
                            }
                            else if (AutomataGep.Elakadva)
                            {
                                AutomataGep.JavitasRugassal();
                                if (!AutomataGep.Elakadva)
                                {
                                    var ujraVasolt = AutomataGep.Vasarlas(termek.Nev, f);
                                    if (ujraVasolt != null)
                                    {
                                        f.Fogyaszt(ujraVasolt);
                                        Console.WriteLine($"[IRODA] {f.Nev} a javítás után sikeresen megvette és elfogyasztotta: {ujraVasolt.Nev}.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("[IRODA] Az automata üres, nincs mit venni!");
                        }
                    }
                }

                // Óra végi státusz színesen
                foreach (var f in Fejlesztok)
                {
                    if (f.Kiegve) Console.ForegroundColor = ConsoleColor.Red;
                    else if (f.StresszSzint > 50) Console.ForegroundColor = ConsoleColor.Yellow;
                    else Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine($"  -> {f.Nev} ({f.Munkakor}): Koffein={f.Koffeinszint}, Stressz={f.StresszSzint}, Pénz={f.Penz} Ft, Kiégve={f.Kiegve}");
                    Console.ResetColor();
                }
            }
        }

        public void NapiJelentes()
        {
            Console.WriteLine("\n================ NAPI JELENTÉS ================");

            var top3Feszult = Fejlesztok.OrderByDescending(f => f.StresszSzint).Take(3);
            Console.WriteLine("A 3 legfeszültebb fejlesztő:");
            foreach (var f in top3Feszult)
            {
                Console.WriteLine($"- {f.Nev} | Stressz: {f.StresszSzint} | Koffein: {f.Koffeinszint} | Kiégve: {f.Kiegve}");
            }

            int kiegtekSzama = Fejlesztok.Count(f => f.Kiegve);
            Console.WriteLine($"\nKiégett vagy túladagolt fejlesztők száma: {kiegtekSzama}");
            Console.WriteLine($"Automata teljes napi bevétele: {AutomataGep.KeszpenzKassza} Ft");
            Console.WriteLine($"Automata maradék készlete: {AutomataGep.Keszlet.Count} db termék");
            Console.WriteLine("===============================================");
        }
    }
}
