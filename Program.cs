
using Microsoft.Data.Sqlite;

namespace SQLiteSample
{
    class Program
    {
        static void Main()
        {

            LemmikkiTietokanta tietokanta = new LemmikkiTietokanta();

            while (true)
            {
                Console.WriteLine("1. Lisää omistaja");
                Console.WriteLine("2. Lisää lemmikki");
                Console.WriteLine("3. Päivitä omistajan puhelinnumero");
                Console.WriteLine("4. Etsi lemmikin omistajan puhelinnumero");
                Console.WriteLine("5. Näytä omistajan id");
                Console.WriteLine("6. Lopeta");

                Console.Write("Valitse toiminto: ");
                var valinta = Console.ReadLine();

                if (valinta == "1")
                {
                    OmistajaLisays.LisaaOmistaja();
                }
                else if (valinta == "2")
                {
                    LemmikkiLisays.LisaaLemmikki();
                }
                else if (valinta == "3")
                {
                    OmistajaPaivitys.PaivitaPuhNro();
                }

                else if (valinta == "4")
                {
                    OmistajanHaku.EtsiPuhNro();
                }
                else if (valinta == "5")
                {
                    OmistajanHaku.NaytaOmistajanId();
                }
                else if (valinta == "6")
                {
                    break;
                }
            }
        }

    }
}
