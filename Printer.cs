using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService
{
    public abstract class Printer
    {
        int dwa;

        /* public int Id { get; private set; }
         public bool Busy { get; private set; }
         public int TotalPaper { get; private set; }
         public int TotalCloth { get; private set; }
         public int TotalOrders { get; private set; }
         public int _timeWorked { get; private set; }
         public PrinterType Type { get; private set; }
         private JobType _jobType;
         private int _taskTime;
         private Random random;

         public Printer(PrinterType type, int id)
         {
             Id = id;
             Type = type;
             _taskTime = 0;
             _timeWorked = 0;
             TotalOrders = 0;
             TotalCloth = 0;
             TotalPaper = 0;
             Busy = false;
             TotalOrders = 0;
             random = new Random();
         }

         public void Print()
         {
             if (!Busy)
                 return;
             if (_taskTime > 0)
             {
                 _taskTime--;
                 _timeWorked++;
             }
             if (_taskTime == 0)
             {
                 if (JobType.Cloth == _jobType)
                     TotalCloth++;
                 if (JobType.Paper == _jobType)
                     TotalPaper++;
                 TotalOrders = TotalCloth + TotalPaper;
                 Busy = false;
                 Console.WriteLine("Принтер({0}) {1}: Заказ выполнен. Кол-во выполненных заказов {2}", GetTypeName(), Id, TotalOrders);
             }
         }

         public void TakeOrder(List<JobType> tasks, int currentMinutes)
         {
             if (Busy)
                 return;
             *//**//*



             int indexToDelete = -1;
             for(var i = 0; i < tasks.Count; i++) 
             {
                 if(Type == PrinterType.Both)
                 {
                     if(tasks[i] == JobType.Paper)
                         _taskTime = 180 + random.Next(-10, 10);
                     else if (tasks[i] == JobType.Cloth)
                         _taskTime = 150 + random.Next(-7, 7);
                     if (_taskTime + currentMinutes >= PrintServiceSimulation.DayLength)
                     {
                         _taskTime = 0;
                         return false;
                     }
                     _jobType = tasks[i];
                     Busy = true;
                     indexToDelete = i;
                     Console.WriteLine("Принтер({0}) {1}: Заказ принят", GetTypeName(), Id);
                     break;
                 }
                 else if(Type == PrinterType.Paper)
                 {
                     if (tasks[i] == JobType.Paper)
                     {
                         _taskTime = 170 + random.Next(-10, 10);
                         if (_taskTime + currentMinutes >= PrintServiceSimulation.DayLength)
                         {
                             _taskTime = 0;
                             return false;
                         }
                         indexToDelete = i;
                         Busy = true;
                         _jobType = tasks[i];
                         Console.WriteLine("Принтер({0}) {1}: Заказ принят", GetTypeName(), Id);
                         break;
                     }
                 }
             }
             if(indexToDelete != -1)
             {
                 tasks.RemoveAt(indexToDelete);
                 return true;
             }
             return false;

         }

         private string GetTypeName()
         {
             if (Type == PrinterType.Paper)
                 return "Бумага";
             return "Универсальный";
         }
     }*/
        public int Id => throw new NotImplementedException();

        public int PaperPrintingTime => throw new NotImplementedException();

        public int PaperPrintingError => throw new NotImplementedException();

        public int TimeWorked => throw new NotImplementedException();

        public int TotalPaperOrders => throw new NotImplementedException();

        public bool Busy => throw new NotImplementedException();

        public void Print()
        {
            throw new NotImplementedException();
        }

        public bool TakeOrder(List<JobType> tasks, int currentMinutes)
        {
            throw new NotImplementedException();
        }
    }
}
