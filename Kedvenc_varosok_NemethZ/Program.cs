using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;   //ezt be kell tölteni

 class Program
 {
    static void Main(string[] args)
    {
        string connectionString;
        MySqlConnection dbConnection;
        MySqlCommand sqlCommand;

        connectionString = "server=localhost;userid=root;password=;database=mo_telepulesek";
        dbConnection = new MySqlConnection(connectionString);
        dbConnection.Open();

        sqlCommand = new MySqlCommand();
        sqlCommand.Connection = dbConnection;
        sqlCommand.CommandText = "DROP TABLE IF EXISTS kvarosok;";
        sqlCommand.ExecuteNonQuery();

        // SCALAR = egy visszatérési értéket várunk előző feladatban volt.
        // EXECUTENONQUERY() = nem várom visszatérési értéket

        // tábla létrehozása:


        Console.ReadKey();
    }
 }
