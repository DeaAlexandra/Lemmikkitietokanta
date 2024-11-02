using System;
using Microsoft.Data.Sqlite;

namespace SQLiteSample
{
    public static class OmistajaLisays
    {
        public static void LisaaOmistaja()
        {
            Console.Write("Anna omistajan nimi: ");
            var nimi = Console.ReadLine();

            Console.Write("Anna puhelinnumero: ");
            var puhelin = Console.ReadLine();

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var insertCmd = connection.CreateCommand();
                // Muutetaan kenttien nimet isolla alkukirjaimella
                insertCmd.CommandText = "INSERT INTO Omistajat(Nimi, Puhelin) VALUES($nimi, $puhelin)";
                insertCmd.Parameters.AddWithValue("$nimi", nimi);
                insertCmd.Parameters.AddWithValue("$puhelin", puhelin);
                insertCmd.ExecuteNonQuery();
            }

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                // Muutetaan kenttien nimet isolla alkukirjaimella
                selectCmd.CommandText = "SELECT Id FROM Omistajat WHERE Nimi = $nimi";
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
