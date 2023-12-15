using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWPF
{
    public interface ICar
    {
        public int Id { get; set; }
        bool EngineIsRunning { get; }
        void BrakeBy(int speed); // car #2
        void Accelerate(int speed); // car #2

        void EngineStart();
        void EngineStop();
        void FreeWheel(); // car #2
        void Refuel(double liters);
        void RunningIdle();
    }
}
