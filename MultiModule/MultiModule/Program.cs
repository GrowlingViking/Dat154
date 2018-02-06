using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;



namespace MultiModule
{
    class Program
    {
        const String sDll2 = "C:\\DAT154\\Forelesning 13 (MVC MVVM)\\MultiModule\\Debug\\ModuleSDK2.dll";
        [DllImport(sDll2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sum(int x, int y);

        const String sDll3 = "C:\\DAT154\\Forelesning 13 (MVC MVVM)\\MultiModule\\Debug\\ModuleSDK3.dll";
        [DllImport(sDll3, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Diff(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        static void Main(string[] args)
        {
            //int sum = 0;

            //sum = Sum(2, 4);

            //int diff = Diff(20, 10);
            //Console.WriteLine("Sum 2 + 4 = " + sum.ToString());
            //Console.WriteLine("Diff 20 - 10 = " + diff.ToString());

            MessageBox(IntPtr.Zero, "SDK message !!!!!!!!!!!!!!!!!!!!!!!!!", "Called directly from SDK", 0);
            Console.Read();
        }
    }
}
