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
using System.Windows.Threading;

namespace UseUrTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();

            TextBlockMinutes.Text = "00";
            TextBlockSeconds.Text = "00";

            dispatcherTimer = new()
            {
                Interval = new TimeSpan(0, 0, 0, 1)
            };
            dispatcherTimer.Tick += DispatcherTimer_Tick;

        }

        private void ResetTimer(string minutes)
        {
            dispatcherTimer.Stop();

            TextBlockMinutes.Text = minutes;
            TextBlockSeconds.Text = "00";

            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if(int.TryParse(TextBlockSeconds.Text, out var seconds) && int.TryParse(TextBlockMinutes.Text, out var minutes))
            {
                if(0 == seconds)
                {
                    if(0 == minutes) 
                    {
                        if(sender is DispatcherTimer dispatcherTimer) 
                        {
                            dispatcherTimer.Stop();
                        }
                    }
                    else
                    {
                        minutes--;
                        seconds = 59;
                    }
                }
                else
                {
                    seconds--;
                }

                TextBlockMinutes.Text = minutes.ToString("00");
                TextBlockSeconds.Text = seconds.ToString("00");
            }
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Stop_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void MenuItem_Timer_Click(object sender, RoutedEventArgs e)
        {
            if(sender is MenuItem menuItem)
            {
                if (menuItem.Header is string header)
                {
                    string[] times = header.Split(':');
                    if(times.Length == 2)
                    {
                        ResetTimer(times[0]);
                    }
                }
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
    }
}
