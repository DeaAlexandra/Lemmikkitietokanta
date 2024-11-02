using System;
using Microsoft.Data.Sqlite;

namespace SQLiteSample
{
    public class LemmikkiTietokanta
    {
        private readonly string _connectionString = "Data Source=Lemmikkitietokanta.db";

        public LemmikkiTietokanta()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            CreateTables(connection);
        }

        private static void CreateTables(SqliteConnection connection)
        {
            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Omistajat(
                    Id INTEGER PRIMARY KEY,
                    Nimi TEXT,
                    Puhelin TEXT
                )";
            createTableCmd.ExecuteNonQuery();

            var createTableCmd2 = connection.CreateCommand();
            createTableCmd2.CommandText = @"
                CREATE TABLE IF NOT EXISTS Lemmikit(
                    Id INTEGER PRIMARY KEY,
                    Nimi TEXT,
                    Rotu TEXT,
                    OmistajaId INT,
                    FOREIGN KEY (OmistajaId) REFERENCES Omistajat(Id)
                )";
            createTableCmd2.ExecuteNonQuery();
        }

        // CRUD-metodit

        // Lisää omistaja
        public void AddOwner(string nimi, string puhelin)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = "INSERT INTO Omistajat(Nimi, Puhelin) VALUES($nimi, $puhelin)";
            insertCmd.Parameters.AddWithValue("$nimi", nimi);
            insertCmd.Parameters.AddWithValue("$puhelin", puhelin);
            insertCmd.ExecuteNonQuery();
        }

        // Lisää lemmikki
        public void AddPet(string nimi, string rotu, int omistajaId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = "INSERT INTO Lemmikit(Nimi, Rotu, OmistajaId) VALUES($nimi, $rotu, $omistajaId)";
            insertCmd.Parameters.AddWithValue("$nimi", nimi);
            insertCmd.Parameters.AddWithValue("$rotu", rotu);
            insertCmd.Parameters.AddWithValue("$omistajaId", omistajaId);
            insertCmd.ExecuteNonQuery();
        }

        // Päivitä omistajan puhelinnumero
        public void UpdateOwnerPhone(int id, string uusiPuhelin)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var updateCmd = connection.CreateCommand();
            updateCmd.CommandText = "UPDATE Omistajat SET Puhelin = $puhelin WHERE Id = $id";
            updateCmd.Parameters.AddWithValue("$puhelin", uusiPuhelin);
            updateCmd.Parameters.AddWithValue("$id", id);
            updateCmd.ExecuteNonQuery();
        }

        // Hae omistajan puhelinnumero lemmikin nimellä
        public string? GetOwnerPhoneByPetName(string lemmikinNimi)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"
                SELECT Omistajat.Puhelin 
                FROM Lemmikit 
                JOIN Omistajat ON Lemmikit.OmistajaId = Omistajat.Id 
                WHERE Lemmikit.Nimi = $lemmikinNimi";
            selectCmd.Parameters.AddWithValue("$lemmikinNimi", lemmikinNimi);

            using var reader = selectCmd.ExecuteReader();
            return reader.Read() ? reader.GetString(0) : null; // Return type is explicitly nullable
        }

        // Hae omistajan id nimen perusteella
        public int? GetOwnerIdByName(string nimi)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT Id FROM Omistajat WHERE Nimi = $nimi";
            selectCmd.Parameters.AddWithValue("$nimi", nimi);

            using var reader = selectCmd.ExecuteReader();
            return reader.Read() ? (int?)reader.GetInt32(0) : null;
        }
    }
}
