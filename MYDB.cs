using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Kasir_Laundry2
{
    public class MYDB : DbContext
    {
        public MYDB()
            : base("name=MYDB")
        {

        }
        private MySqlConnection connection = new MySqlConnection(
           "server=localhost;port=3306;username=root;password=;database=kasir_laundry");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
        public DataTable getData(string query, MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand(query, getConnection());
            command.Parameters.Add(parameters);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            
            return table;
        }
    }
    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}