using System;
using Microsoft.Data.Sqlite;

namespace SQLiteSample
{
    public static class OmistajanHaku
    {
        public static void EtsiPuhNro()
        {
            Console.Write("Anna lemmikin nimi: ");
            var lemmikinNimi = Console.ReadLine(); // Määritä lemmikinNimi tässä

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                // Suoritetaan kysely, joka yhdistää Lemmikit ja Omistajat taulut omistaja_id:n perusteella
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"
                    SELECT Omistajat.Puhelin 
                    FROM Lemmikit 
                    JOIN Omistajat ON Lemmikit.Omistaja_Id = Omistajat.Id 
                    WHERE Lemmikit.Nimi = $lemmikinNimi;"; // Oikea JOIN taulujen välillä omistaja_id:n perusteella
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

        public static void NaytaOmistajanId()
        {
            Console.Write("Anna omistajan nimi: ");
            var nimi = Console.ReadLine();

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT Id FROM Omistajat WHERE Nimi = $nimi"; // Muutettu Id ja Nimi isolla alkukirjaimella
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
