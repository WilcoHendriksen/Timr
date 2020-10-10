using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace timr
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private readonly DispatcherTimer dt;
    private TimeSpan time;

    public MainWindow()
    {
      InitializeComponent();
      dt = new DispatcherTimer();
      time = TimeSpan.Zero;
      TimerLabel.Content = time.ToString(@"mm\:ss");
    }

    private void DragMove(object sender, MouseButtonEventArgs e)
    {
      try
      {
        DragMove();
      }
      catch (Exception ex)
      {
        // don't do anything...
      }
    }

    private void timr_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.S)
      {
        dt.Start();
      }

      if (e.Key == Key.R)
      {
        time = TimeSpan.Zero;
      }

      if (e.Key == Key.T)
      {
        time = TimeSpan.FromMinutes(45);
      }

      if (e.Key == Key.X)
      {
        dt.Stop();
      }

      if (e.Key == Key.Right)
      {
        time += TimeSpan.FromMinutes(1);
      }

      if (e.Key == Key.Left)
      {
        time -= TimeSpan.FromMinutes(1);
      }

      if (e.Key == Key.Up)
      {
        TimerLabel.FontSize++;
      }

      if (e.Key == Key.Down)
      {
        TimerLabel.FontSize--;
      }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      dt.Interval = TimeSpan.FromSeconds(1);
      dt.Tick += (o, args) =>
      {
        time += TimeSpan.FromSeconds(1);
        TimerLabel.Content = time.ToString(@"mm\:ss");
      };
    }
  }
}