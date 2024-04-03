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
        string connectionString, varosnev, megye;
        MySqlConnection dbConnection;
        MySqlCommand sqlCommand;
        int db, i, ev;

        connectionString = "server=localhost;userid=root;password=;database=mo_telepulesek";
        dbConnection = new MySqlConnection(connectionString);
        dbConnection.Open();

        sqlCommand = new MySqlCommand();
        sqlCommand.Connection = dbConnection;
        sqlCommand.CommandText = "DROP TABLE IF EXISTS kvarosok;";
        sqlCommand.ExecuteNonQuery();       //ez lefuttatja a felette lévő sort

        // SCALAR = egy visszatérési értéket várunk előző feladatban volt.
        // EXECUTENONQUERY() = nem várom visszatérési értéket

        // tábla létrehozása: ELSŐ verzió:
        /*sqlCommand.CommandText = "CREATE TABLE kvarosok(" +
                                    "nev varchar(255) NOT NULL," +
                                    "megye varchar(255)," +
                                    "hozzaadva int," +
                                    "PRIMARY KEY(nev)" +
                                ")" +
                                "CHARACTER SET utf8 " +
                                "COLLATE utf8_hungarian_ci; ";      */

        // tábla létrehozása: MÁSODIK verzió:
        sqlCommand.CommandText = "CREATE TABLE kvarosok(nev varchar(255) NOT NULL,megye varchar(255),hozzaadva int,PRIMARY KEY(nev)) CHARACTER SET utf8 COLLATE utf8_hungarian_ci;";
        sqlCommand.ExecuteNonQuery();       //ez lefuttatja a felette lévő sort


        Console.WriteLine("Add meg, hogy mennyi várost szeretnél felvenni: ");
        db = int.Parse(Console.ReadLine());

        //for ciklussal az adatok beolvasása, program elején stringet megadni: varosnev,megye, intként: ev
        for (i = 0; i < db; i++)
        {
            Console.WriteLine("\n" + (i + 1) + ". város felvétele:");
            Console.WriteLine("A város neve: ");
            varosnev = Console.ReadLine();
            Console.WriteLine("A város megyéje: ");
            megye = Console.ReadLine();
            Console.WriteLine("A hozzáadás éve: ");
            ev = int.Parse(Console.ReadLine());


            //SQL parancs következik:
            //sqlCommand.CommandText = "INSERT INTO kvarosok (nev, megye, hozzaadva) VALUES ('" + varosnev + "','" + megye + "'," + ev + ");";
            //sqlCommand.ExecuteNonQuery();

            //vagy {} fűzve:
            sqlCommand.CommandText = $"INSERT INTO kvarosok (nev, megye, hozzaadva) VALUES ('{varosnev}','{megye}',{ev});";
            sqlCommand.ExecuteNonQuery();       //ez lefuttatja a felette lévő sort
        }

        Console.WriteLine("A városok rögzítése megtörént.");

        //vagy Előkészített parancsok használatával
        //helykitöltő (placeholder) elemeket használunk az értékek helyett és majd később adjuk meg a tényleges értékeket.
        sqlCommand.CommandText = "INSERT INTO kvarosok (nev, megye, hozzaadva) VALUES (@vnev, @vmegye, @vev);";
        sqlCommand.Parameters.AddWithValue("@vnev", "Szentendre");
        sqlCommand.Parameters.AddWithValue("@vmegye", "Pest");
        sqlCommand.Parameters.AddWithValue("@vev", 2024);

        //ezeket a paramétereket össze kell gyúrni
        sqlCommand.Prepare();
        sqlCommand.ExecuteNonQuery();       //ez lefuttatja a felette lévő sort

        dbConnection.Close();

        Console.ReadKey();
    }
 }
