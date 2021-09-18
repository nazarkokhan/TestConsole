using System;

namespace TestConsole
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Net.Http;

    class Program
    {

        static void Main()
        {
            var str = "wtf man";
            int intValue;
            Structure a = new Structure();
            object ob = str;
            Console.WriteLine(ob.ToString());

            Console.ReadLine();
        }
    }

    public ref struct Structure
    {
        public int _a;

        public Structure(int a)
        {
            _a = a;
        }
    }

    public abstract class AbstractClass
    {
        
    }
}