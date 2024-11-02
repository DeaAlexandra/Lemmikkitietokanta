using System;
using Microsoft.Data.Sqlite;

namespace SQLiteSample
{
    public static class OmistajaPaivitys
    {
        public static void PaivitaPuhNro()
        {
            Console.Write("Anna omistajan id: ");
            var id = Console.ReadLine();

            Console.Write("Anna uusi puhelinnumero: ");
            var puhelin = Console.ReadLine();

            using (var connection = new SqliteConnection("Data Source=Lemmikkitietokanta.db"))
            {
                connection.Open();

                var updateCmd = connection.CreateCommand();
                // Kenttien nimet muutettu isolla alkukirjaimella
                updateCmd.CommandText = "UPDATE Omistajat SET Puhelin = $puhelin WHERE Id = $id";
                updateCmd.Parameters.AddWithValue("$puhelin", puhelin);
                updateCmd.Parameters.AddWithValue("$id", id);
                updateCmd.ExecuteNonQuery();
            }
        }
    }
}
