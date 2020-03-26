using System;
using System.Data;
using System.Data.SqlClient;

namespace Laborator____1
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr = "Data Source = DESKTOP-KCS7V0D\\SQLEXPRESS; Initial Catalog = Nail_Salon;" + "Integrated Security = true;";
            SqlConnection comm = new SqlConnection(conStr);
            // sql commands
            string query = "select * from Client";
            //SqlCommand cmd = new SqlCommand(query,comm);
            //comm.Open();
            //// SQL data reader
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine(String.Format("{0},{1}", reader[0],reader[1]));
            //}

            SqlDataAdapter da = new SqlDataAdapter(query,comm);
            DataSet dset = new DataSet();
            da.Fill(dset, "Client");
            foreach (DataRow row in dset.Tables["CLient"].Rows) { Console.WriteLine("{0}, {1}", row["CID"], row["FirstName"]); }

            Console.ReadKey();
            // lucram in form1
            // In Windows form app creaza o procedura fillData(Data grid view ca si control- recomanda)
            // Sql connection
            //o metoda care da connection string
            // apoi create new sqlDataAdaptrs -- (unul pt fiecare tabel
            // create data set (unul pt toate tabelele)
            //populate the data set (dataele din cele 2 tabele (tabela parinte tabel fiu - din lab 1) si relations between the 2 tables)
            // un alt obiect de tip realtion pe care il punem in data set(se face in sapt 3) *(conetarerea tabelelor prin relatiile existente -- cheie straina)
            //afisam datele + add,delete,update

        }
    }
}
