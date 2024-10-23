
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
                    LisaaOmistaja();
                }
                else if (valinta == "2")
                {
                    LisaaLemmikki();
                }
                else if (valinta == "3")
                {
                    PaivitaPuhNro();
                }

                else if (valinta == "4")
                {
                    EtsiPuhNro();
                }
                else if (valinta == "5")
                {
                    NaytaOmistajanId();
                }
                else if (valinta == "6")
                {
                    break;
                }
            }
        }




        static void LisaaOmistaja()
        {
            Console.Write("Anna omistajan nimi: ");
            var nimi = Console.ReadLine();

            Console.Write("Anna puhelinnumero: ");
            var puhelin = Console.ReadLine();

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var insertCmd = connection.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Omistajat(nimi, puhelin) VALUES($nimi, $puhelin)";
                insertCmd.Parameters.AddWithValue("$nimi", nimi);
                insertCmd.Parameters.AddWithValue("$puhelin", puhelin);
                insertCmd.ExecuteNonQuery();
            }

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT id FROM Omistajat WHERE nimi = $nimi";
                selectCmd.Parameters.AddWithValue("$nimi", nimi);

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Omistajan id on {reader.GetInt32(0)}");
                    }
                }
            }
        }

        static void LisaaLemmikki()
        {
            Console.Write("Anna lemmikin nimi: ");
            var nimi = Console.ReadLine();

            Console.Write("Anna lemmikin rotu: ");
            var rotu = Console.ReadLine();

            Console.Write("Anna omistajan id: ");
            var omistajaId = Console.ReadLine();

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var insertCmd = connection.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Lemmikit(nimi, rotu, omistaja_id) VALUES($nimi, $rotu, $omistajaId)";
                insertCmd.Parameters.AddWithValue("$nimi", nimi);
                insertCmd.Parameters.AddWithValue("$rotu", rotu);
                insertCmd.Parameters.AddWithValue("$omistajaId", omistajaId);
                insertCmd.ExecuteNonQuery();
            }
        }

        static void PaivitaPuhNro()
        {
            Console.Write("Anna omistajan id: ");
            var id = Console.ReadLine();

            Console.Write("Anna uusi puhelinnumero: ");
            var puhelin = Console.ReadLine();

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var updateCmd = connection.CreateCommand();
                updateCmd.CommandText = "UPDATE Omistajat SET puhelin = $puhelin WHERE id = $id";
                updateCmd.Parameters.AddWithValue("$puhelin", puhelin);
                updateCmd.Parameters.AddWithValue("$id", id);
                updateCmd.ExecuteNonQuery();
            }
        }

        static void EtsiPuhNro()
        {
            Console.Write("Anna lemmikin nimi: ");
            var lemmikinNimi = Console.ReadLine(); // Määritä lemmikinNimi tässä

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                // Suoritetaan kysely, joka yhdistää Lemmikit ja Omistajat taulut omistaja_id:n perusteella
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"
                    SELECT Omistajat.puhelin 
                    FROM Lemmikit 
                    JOIN Omistajat ON Lemmikit.omistaja_id = Omistajat.id 
                    WHERE Lemmikit.nimi = $lemmikinNimi;"; // Oikea JOIN taulujen välillä omistaja_id:n perusteella
                selectCmd.Parameters.AddWithValue("$lemmikinNimi", lemmikinNimi); // Parametri lemmikin nimelle

                using (var reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read()) // Tarkistetaan, onko tuloksia
                    {
                        Console.WriteLine($"Omistajan puhelinnumero on {reader.GetString(0)}");
                    }
                    else
                    {
                        Console.WriteLine("Lemmikkiä ei löytynyt tai sillä ei ole omistajaa.");
                    }
                }
            }
        }

        static void NaytaOmistajanId()
        {
            Console.Write("Anna omistajan nimi: ");
            var nimi = Console.ReadLine();

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT id FROM Omistajat WHERE nimi = $nimi";
                selectCmd.Parameters.AddWithValue("$nimi", nimi);

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Omistajan id on {reader.GetInt32(0)}");
                    }
                }
            }
        }


    }
}
