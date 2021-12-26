using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrintService
{
    internal class Program
    { 
        static PrintServiceSimulation CreatePreset(int paper, int both)
        {
            PrintServiceSimulation simulation = new PrintServiceSimulation();
            var printerCounter = 0;
            for (int i = 0; i < paper; i++)
                simulation.AddPrinter(new PaperPrinter(++printerCounter));
            for (int i = 0; i < both; i++)
                simulation.AddPrinter(new PaperClothPrinters(++printerCounter));
            return simulation;
        }

        static void Main(string[] args)
        {
            Random rnd = new Random(1337);
            for(int i = 0; i < 20; i++)
            {
                var sim = CreatePreset(8 , 5);
                sim.Start();
                Console.WriteLine("--------------------------");
            }
            Console.Read();
        }
    }
}
