using master.Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace master
{
    class Program
    {
        static void Main(string[] args)
        {
            IAlgorithm[] algs = { /*new SymmetricAlgorithm(), */new AsymmetricAlgorithm()/*, new GroupBroadcastAlgorithm() */};
            string[] files = Report.GetFiles();

            foreach (var file in files)
            {
                foreach (var alg in algs)
                {
                    Console.WriteLine("==========\nReading file: " + file + " with algorithm " + alg.GetType().ToString());
                    var ret = Report.ProcessFile(file, alg);
                    Console.WriteLine(JsonConvert.SerializeObject(ret));
                    Console.WriteLine("Finishing file: " + file);
                }
            }

            return;
        }
    }
}
