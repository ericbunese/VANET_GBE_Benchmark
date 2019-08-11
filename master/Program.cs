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
            string [] files = Report.GetFiles();
            foreach (var file in files)
            { 
                Console.WriteLine("==========\nReading file: "+file);
                var ret = Report.ProcessFile(file, new SymmetricAlgorithm());
                Console.WriteLine(JsonConvert.SerializeObject(ret));
                Console.WriteLine("Finishing file: "+file);
            }

            return;
        }
    }
}
