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
        None, Paper, Cloth
    };

    public struct Result
    {
        List<Tuple<float, IPrinter>> printers;
        
    }



    public class PrintServiceSimulation
    {
        public readonly static int DayLength = 600;
        public readonly static int Days = 7;
        private int _minutes;
        private int _nextOrder;
        private int _rejectedOrders;
        private Random _random;
        private List<IPrinter> _printers;
        private List<JobType> _tasks;
        private int _day;

        public PrintServiceSimulation()
        {
            _printers = new List<IPrinter>();
            _tasks = new List<JobType>();
            _random = new Random();
            _day = 0;
            _minutes = 0;
            _nextOrder = 0;
            _rejectedOrders = 0;
        }

        public void AddPrinter(IPrinter printer)
        {
            _printers.Add(printer);
        }

        public void Start()
        {

            //Console.WriteLine("Симуляция начата");
            while (_day < Days)
            {
                DoOrder();
                UpdatePrinters();
                NextStep();
            }
            //Console.WriteLine("Симуляция закончена");
            ShowResults();
        }

        private void NextStep()
        {
            if (_minutes >= DayLength)
            {
                _day++;
                _minutes = 0;
                //Console.WriteLine("День {0} прошел", _day);
            }
            else
            {
                _minutes++;
            }
        }

        public void ShowResults()
        {
            Console.WriteLine("Показатель эффективности работы");
            foreach (var printer in _printers)
            {
                var timers = printer.GetTimesWorked();
                var min = (int)((timers.Min() / (double)DayLength) * 100);
                var max = (int)((timers.Max() / (double)DayLength) * 100);
                var effectivity = (int)((float)printer.GetTimeWorked() / (_day * DayLength) * 100);
                //if(printer.Type == Printer.PrinterType.Paper)
                Console.WriteLine("Принтер({4}) {0}: Мин.Загрузка: {1}%, Сред.Загрузка: {2}%, Выс.Загрузка: {3}% ", printer.Id, min, effectivity, max, printer.GetName());
            }

            var totalClientPaper = _printers.Sum(p => p.GetTotalPaperOrders());
            var totalClientCloth = _printers.Sum(p => p.GetTotalClothOrders());
            var totalClient = totalClientPaper + totalClientCloth;
            Console.WriteLine("\tКол-во выполненых заказов: {0}", totalClient);
            Console.WriteLine("\tКол-во заказов на печать бумаги: {0}", totalClientPaper);
            Console.WriteLine("\tКол-во заказов на печать на ткани: {0}", totalClientCloth);
            Console.WriteLine("\tКол-во отказов: {0}", _rejectedOrders);
            Console.WriteLine("\tПроцент обслуженных заказов: {0}%", (int)((float)totalClient / (totalClient + _rejectedOrders) * 100));
        }

        public void UpdatePrinters()
        {
            foreach (var printer in _printers.Where(p => p.IsBusy()))
                printer.Print(_day);
            foreach (var printer in _printers.Where(p => !p.IsBusy()).OrderBy(p => p.GetTimeWorked()))
            {
                if (printer.TakeOrder(_tasks, _minutes))
                    break;
            }
        }

        public bool AnyFreePrinter()
        {
            foreach (var printer in _printers)
            {
                if (!printer.IsBusy())
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
               // Console.WriteLine("Заказ добавлен в очередь {0}", task == JobType.Paper ? "Бумага" : "Ткань");
            }
            else
            {
                _rejectedOrders++;
               // Console.WriteLine("Заказ не принят. Всё занято");
            }
        }

        private int GetNextOrderTime()
        {
            var time = _minutes + 15 + _random.Next(-5, 5);
            if (time >= DayLength)
                return 15 + _random.Next(-5, 5);
            return time;
        }

    }
}
