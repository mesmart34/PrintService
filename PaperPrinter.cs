using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService
{
    class PaperPrinter : IPrinter
    {
        public int Id { get; }
        public int PaperPrintingTime { get { return 170; } }
        public int PaperPrintingError { get { return 10; } }
        public int[] _dayTimeWorked { get; }
        private bool _busy;
        private int _totalPaperOrders;
        private int _jobTime;
        private int _stayTime;
        private Random random;
        private JobType _currentJob;

        public PaperPrinter(int id)
        {
            Id = id;
            _totalPaperOrders = 0;
            _busy = false;
            random = new Random();
            _dayTimeWorked = new int[PrintServiceSimulation.Days];
        }

        public void Print(int day)
        {
            if (!_busy)
            {
                _stayTime++;
                return;                                                                     
            }
            _dayTimeWorked[day]++;
            _jobTime--;
            if (_jobTime == 0)
            {
                _busy = false;
                _totalPaperOrders++;
                _currentJob = JobType.None;
                //Console.WriteLine("Принтер(Бумага) {0}: Заказ выполнен. Кол-во выполненных заказов {1}", Id, GetTotalPaperOrders());
            }
        }

        public bool TakeOrder(List<JobType> tasks, int currentMinutes)
        {
            if (_busy)
                return false;
            var jobs = tasks.Where(jobType => jobType == JobType.Paper).ToList();
            if (jobs.Count == 0)
                return false;
            _currentJob = jobs.First();
            tasks.Remove(_currentJob);
            _jobTime = PaperPrintingTime + random.Next(-PaperPrintingError, PaperPrintingError);
           /* if (currentMinutes + _jobTime > PrintServiceSimulation.DayLength)
                return false;*/
            tasks.Remove(_currentJob);
            _busy = true;
            //Console.WriteLine("Принтер(Бумага) {0}: Заказ принят ({1})", Id, "Бумага");
            return true;
        }

        public int GetTimeWorked() => _dayTimeWorked.Sum(t => t);

        public int GetTotalPaperOrders() => _totalPaperOrders;

        public int GetTotalClothOrders() => 0;

        public bool IsBusy() => _busy;

        public int[] GetTimesWorked()
        {
            return _dayTimeWorked;
        }

        public string GetName()
        {
            return "Бумага";
        }
    }
}
