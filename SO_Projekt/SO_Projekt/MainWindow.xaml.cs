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
using System.Globalization;

namespace SO_Projekt
{

    public partial class MainWindow : Window
    {

        static List<Rectangle> carsL = new List<Rectangle>();
        static List<Rectangle> carsR = new List<Rectangle>();
        DispatcherTimer cartimerL = new DispatcherTimer();
        DispatcherTimer cartimerR = new DispatcherTimer();
        static int[,] coordinatesL;
        static int[,] coordinatesR;
        static List<int> listaAutKoordynatL = new List<int>();
        static List<int> listaAutKoordynatR = new List<int>();

        private delegate void drawCarDelegate(Canvas canvas, double w, double h, double x, double y, Brush brush, bool right);
        public static void DrawCar(Canvas canvas, double w, double h, double x, double y, Brush brush, bool right)
        {
            if (!canvas.Dispatcher.CheckAccess())
            {
                canvas.Dispatcher.Invoke(new drawCarDelegate(DrawCar), new object[] { canvas, w, h, x, y, brush, right });
            }
            else
            {
                Rectangle car = new Rectangle();
                car.Width = w;
                car.Height = h;
                car.Fill = brush;
                canvas.Children.Add(car);
                Canvas.SetLeft(car, x);
                Canvas.SetTop(car, y);

                if(right == true)
                {
                    carsR.Add(car);
                    listaAutKoordynatR.Add(0);
                }
                else
                {
                    carsL.Add(car);
                    listaAutKoordynatL.Add(0);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            GetCoordinates();

            Random random = new Random();
            Random leftRight = new Random();
            for (int i = 0; i < 7; i++)
            {
                if (leftRight.Next(1, 3)==1)
                {
                    int num = random.Next(300, 600);
                    MainWindow.DrawCar(mapa, 10, 10, num, 400, Brushes.Red, true);
                }
                else
                {
                    int num = random.Next(50, 100);
                    MainWindow.DrawCar(mapa, 10, 10, num, 165, Brushes.Red, false);
                }
            }

            cartimerL.Tick += CarTimerEventL;
            cartimerL.Interval = TimeSpan.FromMilliseconds(20);
            cartimerL.Start();

            cartimerR.Tick += CarTimerEventR;
            cartimerR.Interval = TimeSpan.FromMilliseconds(20);
            cartimerR.Start();
        }

        private void GetCoordinates()
        {
           
            coordinatesL = new int[2, mapa.Children.Count];
            coordinatesR = new int[2, mapa.Children.Count];
            int elementL = 0;
            int elementR = 0;

            foreach ( Rectangle rect in mapa.Children)
            {
                if (rect.Tag.Equals("L"))
                {
                    coordinatesL[0, elementL] = (int)Canvas.GetLeft(rect);
                    coordinatesL[1, elementL] = (int)Canvas.GetTop(rect);
                    elementL++;
                }
                else
                {
                    coordinatesR[0, elementR] = (int)Canvas.GetLeft(rect);
                    coordinatesR[1, elementR] = (int)Canvas.GetTop(rect);
                    elementR++;
                }
            }
        }

        #region Sterowanie
        private void CarTimerEventR(object sender, EventArgs e)
        {
            foreach (var movingcar in carsR)
            {
                if (listaAutKoordynatR[carsR.IndexOf(movingcar)] <= 10)
                {

                    //left
                    if (coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]] < Canvas.GetLeft(movingcar))
                    {
                        Canvas.SetLeft(movingcar, Canvas.GetLeft(movingcar) - 10);
                        if (coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]] > Canvas.GetLeft(movingcar))
                        {
                            Canvas.SetLeft(movingcar, coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]]);
                        }

                    }

                    //right
                    else if (coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]] > Canvas.GetLeft(movingcar))
                    {
                        Canvas.SetLeft(movingcar, Canvas.GetLeft(movingcar) + 10);
                        if (coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]] < Canvas.GetLeft(movingcar))
                        {
                            Canvas.SetLeft(movingcar, coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]]);
                        }
                    }

                    //top
                    if (coordinatesR[1, listaAutKoordynatR[carsR.IndexOf(movingcar)]] < Canvas.GetTop(movingcar))
                    {
                        Canvas.SetTop(movingcar, Canvas.GetTop(movingcar) - 10);
                        if (coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]] > Canvas.GetTop(movingcar))
                        {
                            Canvas.SetTop(movingcar, coordinatesR[1, listaAutKoordynatR[carsR.IndexOf(movingcar)]]);
                        }

                    }

                    //bottom
                    if (coordinatesR[1, listaAutKoordynatR[carsR.IndexOf(movingcar)]] > Canvas.GetTop(movingcar))
                    {
                        Canvas.SetTop(movingcar, Canvas.GetTop(movingcar) + 10);
                        if (coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]] < Canvas.GetTop(movingcar))
                        {
                            Canvas.SetTop(movingcar, coordinatesR[1, listaAutKoordynatR[carsR.IndexOf(movingcar)]]);
                        }

                    }

                    if (coordinatesR[1, listaAutKoordynatR[carsR.IndexOf(movingcar)]] == Canvas.GetTop(movingcar) && coordinatesR[0, listaAutKoordynatR[carsR.IndexOf(movingcar)]] == Canvas.GetLeft(movingcar))
                    {
                        listaAutKoordynatR[carsR.IndexOf(movingcar)]++;
                    }
                }

            }
        }

        private void CarTimerEventL(object sender, EventArgs e)
        {
            foreach (var movingcar in carsL)
            {
                if (listaAutKoordynatL[carsL.IndexOf(movingcar)] <= 10)
                {

                    //left
                    if (coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]] < Canvas.GetLeft(movingcar))
                    {
                        Canvas.SetLeft(movingcar, Canvas.GetLeft(movingcar) - 10);
                        if (coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]] > Canvas.GetLeft(movingcar))
                        {
                            Canvas.SetLeft(movingcar, coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]]);
                        }

                    }

                    //right
                    else if (coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]] > Canvas.GetLeft(movingcar))
                    {
                        Canvas.SetLeft(movingcar, Canvas.GetLeft(movingcar) + 10);
                        if (coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]] < Canvas.GetLeft(movingcar))
                        {
                            Canvas.SetLeft(movingcar, coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]]);
                        }
                    }

                    //top
                    if (coordinatesL[1, listaAutKoordynatL[carsL.IndexOf(movingcar)]] < Canvas.GetTop(movingcar))
                    {
                        Canvas.SetTop(movingcar, Canvas.GetTop(movingcar) - 10);
                        if (coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]] > Canvas.GetTop(movingcar))
                        {
                            Canvas.SetTop(movingcar, coordinatesL[1, listaAutKoordynatL[carsL.IndexOf(movingcar)]]);
                        }

                    }

                    //bottom
                    if (coordinatesL[1, listaAutKoordynatL[carsL.IndexOf(movingcar)]] > Canvas.GetTop(movingcar))
                    {
                        Canvas.SetTop(movingcar, Canvas.GetTop(movingcar) + 10);
                        if (coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]] < Canvas.GetTop(movingcar))
                        {
                            Canvas.SetTop(movingcar, coordinatesL[1, listaAutKoordynatL[carsL.IndexOf(movingcar)]]);
                        }

                    }

                    if (coordinatesL[1, listaAutKoordynatL[carsL.IndexOf(movingcar)]] == Canvas.GetTop(movingcar) && coordinatesL[0, listaAutKoordynatL[carsL.IndexOf(movingcar)]] == Canvas.GetLeft(movingcar))
                    {
                        listaAutKoordynatL[carsL.IndexOf(movingcar)]++;
                    }
                }

            }
        }
        #endregion
    }
}
