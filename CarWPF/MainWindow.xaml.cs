using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarWPF{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        
        Car car = new Car(15);
        private int time = 0;
        private Car.ActionsToPerform nextAction = Car.ActionsToPerform.NotRunning;
        
        
        public MainWindow(){
            InitializeComponent();

            mainWindow.PreviewKeyDown += (sender, e) => {
                if (e.Key == Key.Up && !car.EngineIsRunning){
                    nextAction = Car.ActionsToPerform.StartEngine;
                }else
                if (e.Key == Key.Up && car.EngineIsRunning){
                    nextAction = Car.ActionsToPerform.Accelerate;
                }else
                if (e.Key == Key.Down && car.EngineIsRunning){
                    nextAction = Car.ActionsToPerform.Brake;
                }else
                if (e.Key == Key.S && car.EngineIsRunning){
                    nextAction = Car.ActionsToPerform.StopEngine;
                }else
                if (e.Key == Key.R && !car.EngineIsRunning){
                    nextAction = Car.ActionsToPerform.Refuel;
                }
                else if (car.EngineIsRunning && car.ActualSpeed > 0){
                    nextAction = Car.ActionsToPerform.RunIdle;
                }
            };

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed +=moveCar;
            timer.Start();
            
        }
        
        public void moveCar (object? sender, ElapsedEventArgs elapsedEventArgs){
            switch (nextAction){
                case Car.ActionsToPerform.Brake:
                    car.performAction(Car.ActionsToPerform.Brake,0);
                    break;
                case Car.ActionsToPerform.Accelerate:
                    car.performAction(Car.ActionsToPerform.Accelerate,0);
                    break;
                case Car.ActionsToPerform.RunIdle:
                    car.performAction(Car.ActionsToPerform.RunIdle,0);
                    break;
                case Car.ActionsToPerform.FreeWheel:
                    break;
                case Car.ActionsToPerform.Refuel:
                    car.performAction(Car.ActionsToPerform.Refuel,5);
                    break;
                case Car.ActionsToPerform.NotRunning:
                    break;
                case Car.ActionsToPerform.StartEngine:
                    car.performAction(Car.ActionsToPerform.StartEngine,0);
                    break;
                case Car.ActionsToPerform.StopEngine:
                    car.performAction(Car.ActionsToPerform.StopEngine,0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            nextAction = Car.ActionsToPerform.RunIdle;
            Dispatcher.Invoke((() => {
                currentSpeedTB.Text = "Time: " + time + "s\n"+car.print();
                time++;
            }));
        }
    }
}