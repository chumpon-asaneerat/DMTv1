﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.Runtime.InteropServices;

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

    public class Passport
    {
        [PrimaryKey]
        public string Identifier { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(typeof(Passport))]
        public string PassportId { get; set; }
        [OneToOne]
        public Passport Passport { get; set; }
        [Ignore]
        public DateTime ExpirationDate
        {
            get { return (null != this.Passport) ? this.Passport.ExpirationDate : DateTime.MinValue; }
        }

    }

    public class Lane
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        [Ignore]
        public string BeginS 
        { 
            get { return this.Begin.ToString("yyyy-MM-dd HH:mm:ss.fff"); } 
            set { }  
        }
        [Ignore]
        public string EndS 
        { 
            get { return this.Begin.ToString("yyyy-MM-dd HH:mm:ss.fff"); } 
            set { } 
        }
    }

    public class Database
    {
        public Database() { }
        ~Database()
        {
            if (null != this.Db)
            {
                this.Db.Dispose();
            }
            this.Db = null;
        }

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
                this.Db.CreateTable<Passport>();
                this.Db.CreateTable<Person>();

                this.Db.CreateTable<Lane>();
            }
        }

        public void Add<T>(T value)
            where T : class
        {
            if (null == this.Db || null == value) return;
            this.Db.Insert(value, typeof(T));
            //this.Db.Insert(value);
        }

        public void Update<T>(T value)
            where T : class
        {
            if (null == this.Db || null == value) return;
            this.Db.Update(value, typeof(T));
            //this.Db.Update(value);
        }

        public T[] Load<T>() 
            where T:class, new()
        {
            var result = this.Db.Table<T>().ToArray();
            return result;
        }

        public void AddStock(string symbol)
        {
            var stock = new Stock()
            {
                Symbol = symbol
            };
            this.Add(stock);
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

        public void InitPersonPassport()
        {
            if (null == this.Db) return;

            if (this.Db.Table<Person>().Count() == 0)
            {
                var passport = new Passport
                {
                    Identifier = "XXYYZZ",
                    ExpirationDate = DateTime.Now.AddDays(365)
                };
                Db.Insert(passport);

                var person = new Person
                {
                    Name = "Joe",
                    Passport = passport
                };
                Db.Insert(person);

                //person.Passport = passport;
                Db.UpdateWithChildren(person);
            }
        }

        public Person[] GetPersons()
        {
            if (null == this.Db) return new Person[] { };
            InitPersonPassport();

            return this.Db.GetAllWithChildren<Person>().ToArray();
        }

        public void AddLanes()
        {
            Lane item;
            item = new Lane()
            {
                Name = "Item 1",
                Begin = new DateTime(2020, 6, 1, 1, 10, 10, 222),
                End = new DateTime(2020, 6, 1, 3, 22, 30, 80)
            };
            Db.Insert(item);
            item = new Lane()
            {
                Name = "Item 2",
                Begin = new DateTime(2020, 6, 1, 3, 30, 10, 250),
                End = new DateTime(2020, 6, 1, 6, 45, 36, 274)
            };
            Db.Insert(item);
            item = new Lane()
            {
                Name = "Item 3",
                Begin = new DateTime(2020, 6, 2, 3, 22, 44, 577),
                End = new DateTime(2020, 6, 2, 6, 16, 12, 512)
            };
            Db.Insert(item);
            item = new Lane()
            {
                Name = "Item 4",
                Begin = new DateTime(2020, 6, 2, 7, 55, 12, 784),
                End = new DateTime(2020, 6, 2, 12, 35, 27, 752)
            };
            Db.Insert(item);
        }
    }
}
