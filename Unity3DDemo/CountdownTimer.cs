using System;
using System.Windows.Threading;
namespace Unity3DDemo
{
    public class CountdownTimer
    {
        private DispatcherTimer _timer;
        private int _countdownValue;

        public event Action<int> OnCountdownUpdated;
        public event Action OnCountdownCompleted;

        public CountdownTimer( int initialTimeInSeconds )
        {
            _countdownValue = initialTimeInSeconds;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds( 1 );
            _timer.Tick += TimerTick;
        }

        public void Start()
        {
            Console.WriteLine( "Countdown Started." );
            _timer.Start();
        }

        public void Pause()
        {
            Console.WriteLine( "Countdown Paused." );
            _timer.Stop();
        }

        public void Cancel()
        {
            Console.WriteLine( "Countdown Canceled." );
            _timer.Stop();
            _countdownValue = 10; // Reset to initial value or any default value you prefer
        }

        private void TimerTick( object sender, EventArgs e )
        {
            if ( _countdownValue > 0 )
            {
                _countdownValue--;
                OnCountdownUpdated?.Invoke( _countdownValue );
                Console.WriteLine( $"Countdown: {_countdownValue}" );
            }
            else
            {
                _timer.Stop();
                Console.WriteLine( "Countdown Complete." );
                OnCountdownCompleted?.Invoke();
            }
        }
    }

}