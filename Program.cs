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
        static void Main(string[] args)
        { 
            PrintServiceSimulation manager1 = new PrintServiceSimulation();
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Paper);
            manager1.AddPrinter(Printer.PrinterType.Both);
            manager1.AddPrinter(Printer.PrinterType.Both);
            manager1.AddPrinter(Printer.PrinterType.Both);
            manager1.AddPrinter(Printer.PrinterType.Both);
            manager1.AddPrinter(Printer.PrinterType.Both);
            manager1.Start();

        }
    }
}
