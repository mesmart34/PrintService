using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService
{
    public class PaperClothPrinters : IPrinter
    {
        public int Id { get; }
        public int PaperPrintingTime { get { return 150; } }
        public int PaperPrintingError { get { return 7; } }
        public int ClothPrintingTime { get { return 180; } }
        public int ClothPrintingError { get { return 10; } }

        private bool _busy;
        private int _totalPaperOrders;
        private int _totalClothOrders;
        private Random random;
        private int _jobTime;
        private JobType _currentJob;
        public int[] _dayTimeWorked { get; }

        public PaperClothPrinters(int id)
        {
            _currentJob = JobType.None;
            Id = id;
            _totalPaperOrders = 0;
            _totalClothOrders = 0;
            _busy = false;
            random = new Random();
            _dayTimeWorked = new int[PrintServiceSimulation.Days];
        }

        public void Print(int day)
        {
            if (!_busy)
                return;
            _jobTime--;
            _dayTimeWorked[day]++;
            if (_jobTime == 0)
            {
                _busy = false;
                if (_currentJob == JobType.Paper)
                    _totalPaperOrders++;
                else if (_currentJob == JobType.Cloth)
                    _totalClothOrders++;
                //Console.WriteLine("Принтер(Универсальный) {0}: Заказ выполнен {1}. Кол-во выполненных заказов {2}", Id, _currentJob == JobType.Paper ? "Бумага" : "Ткань", GetTotalPaperOrders());
                _currentJob = JobType.None;
            }
        }

        public bool TakeOrder(List<JobType> tasks, int currentMinutes)
        {
            if (_busy)
                return false;

            var jobs = tasks.ToList();
            if (jobs.Count == 0)
                return false;
            _currentJob = jobs.First();
            if(_currentJob == JobType.Paper)
                _jobTime = PaperPrintingTime + random.Next(-PaperPrintingError, PaperPrintingError);
            else if(_currentJob == JobType.Cloth)
                _jobTime = ClothPrintingTime + random.Next(-ClothPrintingError, ClothPrintingError);
            /*if (currentMinutes + _jobTime > PrintServiceSimulation.DayLength)
                return false;*/
            tasks.Remove(_currentJob);
            _busy = true;
            //Console.WriteLine("Принтер(Универсальный) {0}: Заказ принят ({1})", Id, _currentJob == JobType.Paper ? "Бумага" : "Ткань");
            return true;
        }

        public int GetTimeWorked() => _dayTimeWorked.Sum(t => t);

        public int[] GetTimesWorked() => _dayTimeWorked;

        public int GetTotalPaperOrders() => _totalPaperOrders;

        public int GetTotalClothOrders() => _totalClothOrders;

        public bool IsBusy() => _busy;

        public string GetName()
        {
            return "Ткань";
        }
    }
}
