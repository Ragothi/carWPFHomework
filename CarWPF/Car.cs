using System;
using System.Text;
using System.Timers;

namespace CarWPF;

public class Car : ICar,IDrivingProcessor,IDrivingInformationDisplay{
    public int Id{ get; set; }
    public bool EngineIsRunning{ get; private set; }
    public int ActualSpeed{ get; set; }
    public int acceleration = 10;
    public double fuelAmount = 50;
    private const double maxFuel = 50;
    private ActionsToPerform currentlyPerformedAction = ActionsToPerform.NotRunning;


    public enum ActionsToPerform{Brake,Accelerate,RunIdle,FreeWheel,Refuel,NotRunning,StartEngine,StopEngine}
    public Car(int acceleration){
        if (acceleration is >= 5 and <= 20){
            this.acceleration = acceleration;
        } else if (acceleration < 5){
            this.acceleration = 5;
        }else{
            this.acceleration = 20;
        }
       
    }

    public void performAction(ActionsToPerform action,double param){
        currentlyPerformedAction = action;
        switch (action){
            case ActionsToPerform.Brake:
                BrakeBy(10);
                break;
            case ActionsToPerform.Accelerate:
                Accelerate(acceleration);
                consumeFuel();
                break;
            case ActionsToPerform.RunIdle:
                RunningIdle();
                break;
            case ActionsToPerform.FreeWheel:
                FreeWheel();
                break;
            case ActionsToPerform.Refuel:
                Refuel(param);
                break;
            case ActionsToPerform.NotRunning:
                break;
            case ActionsToPerform.StartEngine:
                EngineStart();
                break;
            case ActionsToPerform.StopEngine:
                EngineStop();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    public string print(){
        
        StringBuilder sb = new StringBuilder();
        sb.Append("Is engine running?\t").Append(EngineIsRunning).Append("\n");
        sb.Append("Actual speed:\t").Append(ActualSpeed).Append("\n");
        sb.Append("Actual fuel:\t").Append(fuelAmount.ToString("0.0000")).Append("\n");
        sb.Append("Performing action:\t").Append(currentlyPerformedAction).Append("\n");
        
        return sb.ToString() ;
    }
    
    public void BrakeBy(int speed){
        ReduceSpeed(10);
    }

    public void Accelerate(int speed){
        IncreaseSpeedTo(ActualSpeed+acceleration);
    }

    public void EngineStart(){
        EngineIsRunning = true;
        ActualSpeed = 0;
    }

    public void EngineStop(){
        EngineIsRunning = false;
    }

    public void FreeWheel(){
        ReduceSpeed(1);
    }

    public void Refuel(double liters){
        if (liters>0 && !EngineIsRunning && ActualSpeed == 0){
            fuelAmount += liters;
            if (fuelAmount>maxFuel){
                fuelAmount = maxFuel;
            }
        }
    }

    public void RunningIdle(){
        consumeFuel();
    }

    private void consumeFuel(){
        if (ActualSpeed is >=1 and <= 60){
            fuelAmount -= 0.002;
        } else if (ActualSpeed is >=61 and <= 100){
            fuelAmount -= 0.0014;
        } else if (ActualSpeed is >=101 and <= 140){
            fuelAmount -= 0.002;
        } else if (ActualSpeed is >=141 and <= 200){
            fuelAmount -= 0.0025;
        } else if (ActualSpeed is >=201 and <= 250){
            fuelAmount -= 0.003;
        } 
    }
    
    public void IncreaseSpeedTo(int speed){
        if (speed is <= 250 and >=0){
            ActualSpeed = speed;
        }
    }

    public void ReduceSpeed(int speed){
        if (ActualSpeed-speed >=0){
            ActualSpeed -= speed;
        }
       
    }
}