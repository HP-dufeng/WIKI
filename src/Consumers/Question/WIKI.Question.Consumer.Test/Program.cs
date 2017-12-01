using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Question.Consumer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new EventBus();

            bus.Start();

            Console.Title = "WIKI.Question.Subscriber";
            Console.WriteLine("总线已启动. 按任意键退出");

            Console.ReadKey();

            bus.Stop();
        }
    }
}
