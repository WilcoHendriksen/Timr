using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Timr
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private readonly DispatcherTimer _dt;
    private TimeSpan _time;

    /// <summary>
    ///   Constructor, initializes dispatcher and time
    /// </summary>
    public MainWindow()
    {
      InitializeComponent();
      _dt = new DispatcherTimer();
      _time = TimeSpan.Zero;
      TimerLabel.Content = _time.ToString(@"mm\:ss");
    }

    /// <summary>
    ///   Get the time in minutes, each hour is converted to 60 minutes
    /// </summary>
    /// <param name="timeSpan">The elapsed time</param>
    /// <returns></returns>
    private string GetTime(TimeSpan timeSpan)
    {
      var minutesFromHours = timeSpan.Hours > 0 ? 60 * timeSpan.Hours : 0;
      return $"{AddZero(minutesFromHours + timeSpan.Minutes)}:{AddZero(timeSpan.Seconds)}";
    }

    /// <summary>
    ///   Adds a zero if
    ///   <para>amount</para>
    ///   is less then 10
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private string AddZero(int amount)
    {
      return amount < 10 ? "0" + amount : amount.ToString();
    }

    /// <summary>
    ///   Handles DragMove event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DragMove(object sender, MouseButtonEventArgs e)
    {
      try
      {
        DragMove();
      }
      catch
      {
        // don't do anything...
      }
    }

    /// <summary>
    ///   Handles key press commands
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Timr_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.S:
          _dt.Start();
          break;
        case Key.R:
          _time = TimeSpan.Zero;
          break;
        case Key.T:
          _time = TimeSpan.FromMinutes(45);
          break;
        case Key.X:
          _dt.Stop();
          break;
        case Key.Right:
          _time += TimeSpan.FromMinutes(1);
          break;
        case Key.Left:
          _time -= TimeSpan.FromMinutes(1);
          break;
        case Key.Up:
          TimerLabel.FontSize++;
          break;
        case Key.Down:
          TimerLabel.FontSize--;
          break;
        case Key.Q:
          Close();
          break;
      }
    }

    /// <summary>
    ///   update the time every second
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      _dt.Interval = TimeSpan.FromSeconds(1);
      _dt.Tick += (o, args) =>
      {
        _time += TimeSpan.FromSeconds(1);
        TimerLabel.Content = GetTime(_time);
      };
    }
  }
}