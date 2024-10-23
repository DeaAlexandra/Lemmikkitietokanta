// Tietokannan logiikka
using Microsoft.Data.Sqlite;
namespace SQLiteSample
{
    public class LemmikkiTietokanta
    {
        private static string connectionString = "Data Source=Lemmikkitietokanta.db";

        public LemmikkiTietokanta()
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            CreateTables(connection);

            connection.Close();
        }

        static void CreateTables(SqliteConnection connection)
        {
            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = "CREATE TABLE IF NOT EXISTS Lemmikit(id INTEGER PRIMARY KEY, nimi TEXT, rotu TEXT, omistaja_id INT)";
            createTableCmd.ExecuteNonQuery();

            var createTableCmd2 = connection.CreateCommand();
            createTableCmd2.CommandText = "CREATE TABLE IF NOT EXISTS Omistajat(id INTEGER PRIMARY KEY, nimi TEXT, puhelin TEXT)";
            createTableCmd2.ExecuteNonQuery();
        }
    }
}