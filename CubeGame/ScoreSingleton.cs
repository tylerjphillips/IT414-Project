using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class ScoreSingleton
    {
        private static ScoreSingleton instance = new ScoreSingleton();
        private ScoreSingleton(){ }

        public static ScoreSingleton Instance
        {
            get { return instance; }
        }

        public void DoSomething()
        {
            Console.WriteLine("called");
            Console.ReadLine();
        }
    }
}
