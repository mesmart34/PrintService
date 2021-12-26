using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService
{
    public interface IPrinter
    {
        int Id { get; }
        void Print(int day);
        bool TakeOrder(List<JobType> tasks, int currentMinutes);

        int GetTimeWorked();
        int[] GetTimesWorked();
        int GetTotalPaperOrders();
        int GetTotalClothOrders();
        bool IsBusy();
        string GetName();
    }
}
