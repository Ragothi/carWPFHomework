using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWPF
{
    public interface IDrivingProcessor // car #2
    {
        int ActualSpeed { get; set; }
        void IncreaseSpeedTo(int speed);
        void ReduceSpeed(int speed);
    }
}
