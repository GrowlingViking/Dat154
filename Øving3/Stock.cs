using System;
using System.Collections.Generic;

namespace Dex {
    // Delegate definition
    public delegate void Alarm(Stock s);

    public class Stock {
        // Event that is fired when 100pt limit is reached
        public event Alarm ev;

        private Random rnd = new Random();

        public int r { get; set; }  // rate
        public String n { get; set; }   // name

        public Stock(String n, int r) {this.n = n; this.r = r;}

        //  Caclulate new rate for this stock object
        //  Fire event if 100 pt limit is passed
        public bool CalcNewRate() {
            int dr = rnd.Next(-35, 45);
            r = r + dr;
            if ((r - dr) / 100 != r / 100) {
                ev(this);   // Fire the event
                return true;
            }
            return false;
        }
    };

    class Person {
        public String n;
        public int price = 10;
        public int amount = 0;

        public Person(String n) { this.n = n; }

        // Subscribe the event Alarm
        public void Subscribe(Stock a) { a.ev += new Alarm(Warning); }

        // Event Handler
        public virtual void Warning(Stock a) {
            Console.WriteLine(
                " Day: " + Dex.day +
                ", Name: " + n +
                ", Stock: " + a.n +
                ", Rate: " + a.r);
            amount += price;
        }
        public void Invoice() {
            Console.WriteLine(" Cust: " + n + " Inv kr :" + amount);
        }
    };

    class Broker : Person {
        // A broker must pay 5 times the price as a normal 
        // person
        public Broker(String n) : base(n) { price = 50; }
    };

    class Dex {

        public static int day { get; private set; } = 1;

        static void Main(string[] args) {
            

            // CREATE STOCKS
            Stock h, d, t;
            List<Stock> stocks = new List<Stock>
            {
                (h = new Stock("HYDRO", 210)),
                (d = new Stock("DNB", 310)),
                (t = new Stock("TELENOR", 410))
            };


            // Create Broker and Persons
            Person k, s, r;
            List<Person> traders = new List<Person>
            {
                (k = new Broker("KÅRE")),
                (s = new Broker("SVEIN")),
                (r = new Person("RICH"))
            };


            // Subscribe on stocks
            k.Subscribe(h);
            k.Subscribe(d);
            s.Subscribe(t);
            r.Subscribe(h);


            // Run simulation for one month
            for (; day < 31; day++) {
                foreach (Stock a in stocks) {
                    if(a.CalcNewRate()) Console.ReadKey();
                }
                
                
            }
            // Invoice the customers
            foreach (Person p in traders)
                p.Invoice();

            Console.Read();
        }
    }
}
