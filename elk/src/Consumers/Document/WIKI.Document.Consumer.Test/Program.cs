using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Document.Consumer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new EventBus();

            bus.Start();

            Console.Title = "WIKI.Document.Subscriber";
            Console.WriteLine("总线已启动. 按任意键退出");

            Console.ReadKey();

            bus.Stop();
        }
    }
}
