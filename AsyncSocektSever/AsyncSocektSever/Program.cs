using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocektSever
{
    class Program
    {
        static void Main(string[] args)
        {
            Serv serv = new Serv();
            serv.Start("127.0.0.1", 1234);
            Console.ReadKey();
        }
    }
}
