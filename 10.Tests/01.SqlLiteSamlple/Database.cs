using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace SqlLiteSamlple
{
    public class Database
    {
        public string FileName { get; set; }

        private string ConnectionString
        {
            get { return "Data Source=" + FileName + ";Version=3"; }
        }

        public void Create()
        {
            string path = Path.Combine("./", FileName);
            if (!File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);
                    string query = @"CREATE TABLE LOGIN(UserName Text(25), Password Text(25));";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;

                    conn.Close();
                }
            }
        }

        public void Add(string UserName, string Password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(conn);
                string query = @"INSERT INTO LOGIN(UserName, Password) VALUES(@username, @password);";
                cmd.CommandText = query;
                cmd.Parameters.Add(new SQLiteParameter("@username", UserName));
                cmd.Parameters.Add(new SQLiteParameter("@password", Password));
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;

                conn.Close();
            }
        }

        public DataTable GetAll()
        {
            DataTable result = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(conn);
                string query = @"SELECT * FROM LOGIN";
                cmd.CommandText = query;
                SQLiteDataAdapter adaptor = new SQLiteDataAdapter();

                adaptor.SelectCommand = cmd;
                adaptor.Fill(result);

                adaptor.Dispose();
                adaptor = null;
                cmd.Dispose();
                cmd = null;

                conn.Close();
            }
            return result;
        }
    }
}
