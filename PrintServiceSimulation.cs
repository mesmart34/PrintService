using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrintService
{

    public enum JobType
    {
        Paper, Cloth
    };

    public class PrintServiceSimulation
    {
        private int _minutes;
        private int _nextOrder;
        private int _rejectedOrders;
        private Random _random;
        private List<Printer> _printers;
        private List<JobType> _tasks;
        private int _day;

        public PrintServiceSimulation()
        {
            _printers = new List<Printer>();
            _tasks = new List<JobType>();
            _random = new Random();
            _day = 0;
            _minutes = 0;
            _nextOrder = 0;
            _rejectedOrders = 0;
        }

        public void AddPrinter(Printer.PrinterType printerType)
        {
            _printers.Add(new Printer(printerType, _printers.Count + 1));
        }

        public void Start()
        {
            Console.WriteLine("Симуляция начата");
            while (_day < 7)
            {
                DoOrder();
                UpdatePrinters();
                NextStep();
            }
            Console.WriteLine("Симуляция закончена");
            ShowResults();
        }

        private void NextStep()
        {
            _minutes++;
            if (_minutes >= 600)
            {
                _day++;
                _minutes = 0;
                Console.WriteLine("День {0} прошел", _day);
            }
        }

        public void ShowResults()
        {
            Console.WriteLine("Показатель эффективности работы");
            foreach (var printer in _printers)
            {
                var effectivity = printer._timeWorked / (_day * 600.0);
                Console.WriteLine("\tЭффективность принтера {0}: {1}%", printer.Id, (int)(effectivity * 100));
            }

            var totalClientPaper = _printers.Select(p => p.TotalPaper).Sum();
            var totalClientCloth = _printers.Select(p => p.TotalCloth).Sum();
            var totalClient = totalClientPaper + totalClientCloth;
            Console.WriteLine("\tКол-во выполненых заказов: {0}", totalClient);
            Console.WriteLine("\tКол-во заказов на печать бумаги: {0}", totalClientPaper);
            Console.WriteLine("\tКол-во заказов на печать на ткани: {0}", totalClientCloth);
            Console.WriteLine("\tКол-во отказов: {0}", _rejectedOrders);
            Console.WriteLine("\tПроцент обслуженных заказов: {0}%", (int)((float)totalClient / (totalClient + _rejectedOrders) * 100));
        }

        public void UpdatePrinters()
        {
            foreach (var printer in _printers)
                printer.Print();
            foreach (var printer in _printers.Where(p => !p.Busy).OrderBy(p => p.TotalOrders))
            {
                if (printer.TakeOrder(_tasks, _minutes))
                    break;
            }
        }

        public bool AnyFreePrinter()
        {
            foreach (var printer in _printers)
            {
                if (!printer.Busy)
                    return true;
            }
            return false;
        }

        private JobType GenerateJob() => _random.Next(10) < 7 ? JobType.Paper : JobType.Cloth;

        private void DoOrder()
        {
            if (_minutes < _nextOrder)
                return;
            _nextOrder = GetNextOrderTime();
            if (AnyFreePrinter())
            {
                var task = GenerateJob();
                _tasks.Add(task);
                Console.WriteLine("Заказ добавлен в очередь {0}", task == JobType.Paper ? "Бумага" : "Ткань");
            }
            else
            {
                _rejectedOrders++;
                Console.WriteLine("Заказ не принят. Всё занято");
            }
        }

        private int GetNextOrderTime()
        {
            var time = _minutes + 15 + _random.Next(-5, 5);
            if (time >= 600)
                return 15 + _random.Next(-5, 5);
            return time;
        }

    }
}
