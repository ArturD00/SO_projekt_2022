using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace SO_Projekt
{

    public partial class MainWindow : Window
    {
        
        List<Rectangle> cars = new List<Rectangle>();
        DispatcherTimer cartimer = new DispatcherTimer();
       static int[,] coordinates = new int[2, 4];
        static int[] carnextcoordinate = new int[2];
        //x
        //y
        //100, 100, 400, 400
        //400, 200, 200, 500

    

        public static void NewCar() 
        {
            
            Random rnd = new Random();
            

            while (true)
            {
                Rectangle car = new Rectangle
                {
                    Width = 50,
                    Height = 50,
                    // Name = Convert.ToString(rectanglenumber),
                    Fill = Brushes.Red
                };
               // cars.Add(car);


                Canvas.SetLeft(car, 100);
                Canvas.SetTop(car, rnd.Next(0,400));
              //  m.mapa.Children.Add(car);
                Thread.Sleep(100);
            }
        }





        public MainWindow()
        {


            InitializeComponent();
            //100, 100, 400, 400
            //400, 200, 200, 500

            coordinates[0, 0] = 100;
            coordinates[0, 1] = 100;
            coordinates[0, 2] = 400;
            coordinates[0, 3] = 400;
            coordinates[1, 0] = 400;
            coordinates[1, 1] = 200;
            coordinates[1, 2] = 200;
            coordinates[1, 3] = 500;
            carnextcoordinate[0] = 0;
            carnextcoordinate[1] = 0;
            //testowe dodanie auta do listy
            //artur
            Rectangle car2 = new Rectangle
        {
                Width = 50,
            Height = 50,
            // Name = Convert.ToString(rectanglenumber),
            Fill = Brushes.Red
            };
            Canvas.SetLeft(car2, 500);
            Canvas.SetTop(car2, 400);
            cars.Add(car2);
            mapa.Children.Add(car2);

            Rectangle car = new Rectangle
            {
                Width = 50,
                Height = 50,
                // Name = Convert.ToString(rectanglenumber),
                Fill = Brushes.Red
            };
            Canvas.SetLeft(car, 600);
            Canvas.SetTop(car, 400);
            cars.Add(car);
            mapa.Children.Add(car);


            //    Thread t1 = new Thread(MainWindow.NewCar);
            //     t1.Start();

            mapa.Focus();
            cartimer.Tick += CarTimerEvent;
            cartimer.Interval = TimeSpan.FromMilliseconds(20);
            cartimer.Start();


        }

        private void CarTimerEvent(object sender, EventArgs e)
        {
            
           
            foreach (var movingcar in cars)
            {
                if (carnextcoordinate[cars.IndexOf(movingcar)] < 4)
                {


                    //left
                    if (coordinates[0, carnextcoordinate[cars.IndexOf(movingcar)]] < Canvas.GetLeft(movingcar))
                    {
                        Canvas.SetLeft(movingcar, Canvas.GetLeft(movingcar) - 10);
                        if (coordinates[0, carnextcoordinate[cars.IndexOf(movingcar)]] >= Canvas.GetLeft(movingcar))
                        {
                            carnextcoordinate[cars.IndexOf(movingcar)]++;
                        }

                    }
                    //right
                    else if (coordinates[0, carnextcoordinate[cars.IndexOf(movingcar)]] > Canvas.GetLeft(movingcar))
                    {
                        Canvas.SetLeft(movingcar, Canvas.GetLeft(movingcar) + 10);
                        if (coordinates[0, carnextcoordinate[cars.IndexOf(movingcar)]] <= Canvas.GetLeft(movingcar))
                        {
                            carnextcoordinate[cars.IndexOf(movingcar)]++;
                        }
                    }
                    //else
                    //{
                    //    carnextcoordinate[cars.IndexOf(movingcar)]++;
                    //}

                    //top
                    if (coordinates[1, carnextcoordinate[cars.IndexOf(movingcar)]] < Canvas.GetTop(movingcar))
                    {
                        Canvas.SetTop(movingcar, Canvas.GetTop(movingcar) - 10);
                        if (coordinates[1, carnextcoordinate[cars.IndexOf(movingcar)]] >= Canvas.GetTop(movingcar))
                        {
                            carnextcoordinate[cars.IndexOf(movingcar)]++;
                        }

                    }
                    //bottom
                    if (coordinates[1, carnextcoordinate[cars.IndexOf(movingcar)]] > Canvas.GetTop(movingcar))
                    {
                        Canvas.SetTop(movingcar, Canvas.GetTop(movingcar) + 10);
                        if (coordinates[1, carnextcoordinate[cars.IndexOf(movingcar)]] <= Canvas.GetTop(movingcar))
                        {
                            carnextcoordinate[cars.IndexOf(movingcar)]++;
                        }

                    }
                }
                    //else
                    //{
                    //    carnextcoordinate[cars.IndexOf(movingcar)]++;
                    //}
                    //Canvas.SetLeft(movingcar, Canvas.GetLeft(movingcar) - 10);
                
            }
        }
    }
}
