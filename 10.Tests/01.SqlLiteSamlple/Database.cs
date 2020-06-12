using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using SQLite;

namespace SqlLiteSamlple
{
    public class Stock
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Symbol { get; set; }
    }

    public class Valuation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int StockId { get; set; }
        public DateTime Time { get; set; }
    }

    public class Database
    {
        public string FileName { get; set; }

        public SQLiteConnection Db { get; private set; }

        public void Create()
        {
            string path = Path.Combine("./", FileName);
            this.Db = new SQLiteConnection(path);
            if (null != this.Db)
            {
                this.Db.CreateTable<Stock>();
                this.Db.CreateTable<Valuation>();
            }
        }

        public void Add<T>(T value)
            where T : class, new()
        {
            if (null == this.Db || null == value) return;
            this.Db.Insert(value, typeof(T));
        }

        public void Update<T>(T value)
            where T : class, new()
        {
            if (null == this.Db || null == value) return;
            this.Db.Update(value, typeof(T));
        }

        public T[] Load<T>() 
            where T:class, new()
        {
            return this.Db.Table<T>().ToArray();
        }

        public void AddStock(string symbol)
        {
            var stock = new Stock()
            {
                Symbol = symbol
            };
            this.Add<Stock>(stock);
            Console.WriteLine("{0} == {1}", stock.Symbol, stock.Id);
        }


        public void UpdateStock(int id, string symbol)
        {
            if (null == this.Db) return;
            var query = this.Db.Table<Stock>().Where(v => v.Id == id);
            var stock = query.First();
            stock.Symbol = symbol;
            this.Db.Update(stock);
        }
    }
}
