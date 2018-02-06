using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateRep
{
    class Program
    {
        public delegate void Delegate(ref int x);
     
        static void Double(ref int x) { x = x * 2; }
        static void Tripple(ref int x) {
            x = x * 3;
        }
        static void Square(ref int x) { x = x * x; }
        static void Quad(ref int x) { x *=4; }
        static void Main(string[] args)
        {
            Delegate d;


            d  =  Double;
            d += Tripple;
            d += Square;
            d += Quad;

            int a = 4;
            d.Invoke(ref a);
         
            Console.WriteLine("(C#) Value is:  " + a.ToString());

            Console.ReadKey();
        }
    }
}
