using System;
using Microsoft.Data.Sqlite;

namespace SQLiteSample
{
    public static class LemmikkiLisays
    {
        public static void LisaaLemmikki()
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
                // Muutetaan kenttien nimet isolla alkukirjaimella
                insertCmd.CommandText = "INSERT INTO Lemmikit(Nimi, Rotu, OmistajaId) VALUES($nimi, $rotu, $omistajaId)";
                insertCmd.Parameters.AddWithValue("$nimi", nimi);
                insertCmd.Parameters.AddWithValue("$rotu", rotu);
                insertCmd.Parameters.AddWithValue("$omistajaId", omistajaId);
                insertCmd.ExecuteNonQuery();
            }
        }
    }
}
