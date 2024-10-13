using System;

namespace Unity3DDemo
{
    public class MessageProcessor
    {
        private CountdownTimer _countdownTimer;

        public MessageProcessor( CountdownTimer countdownTimer )
        {
            _countdownTimer = countdownTimer;
        }

        public void ProcessMessage( string message )
        {
            switch ( message )
            {
                case "StartCountdown":
                    _countdownTimer.Start();
                    break;
                case "PauseCountdown":
                    _countdownTimer.Pause();
                    break;
                case "CancelCountdown":
                    _countdownTimer.Cancel();
                    break;
                default:
                    Console.WriteLine( "Unknown command received." );
                    break;
            }
        }
    }
}