using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace Unity3DDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpServer _tcpServer;
        private CountdownTimer _countdownTimer;
        private MessageProcessor _messageProcessor;

        public MainWindow()
        {
            InitializeComponent();

            _countdownTimer = new CountdownTimer( 10 ); // Initial countdown value
            _tcpServer = new TcpServer();
            _messageProcessor = new MessageProcessor( _countdownTimer );

            // Subscribing to TcpServer events
            _tcpServer.OnConnect += ClientConnected;
            _tcpServer.OnDisconnect += ClientDisconnected;
            _tcpServer.OnMessageReceived += _messageProcessor.ProcessMessage;
            _tcpServer.OnMessageSend += MessageSent;

            // Subscribing to CountdownTimer events
            _countdownTimer.OnCountdownUpdated += CountdownUpdated;
            _countdownTimer.OnCountdownCompleted += CountdownCompleted;

            StartServer();
        }

        private async void StartServer()
        {
            await _tcpServer.StartListeningAsync( 8080 );
            Console.WriteLine( "Server started, listening on port 8080." );
        }

        private void ClientConnected()
        {
            Console.WriteLine( "Client connected." );
            // Perform additional actions when a client connects
        }

        private void ClientDisconnected()
        {
            Console.WriteLine( "Client disconnected." );
            // Perform additional actions when a client disconnects
        }

        private async void CountdownUpdated( int remainingTime )
        {
            Console.WriteLine( $"Countdown Update: {remainingTime}" );
            await _tcpServer.SendMessage( $"CountdownProgress:{remainingTime}" );
        }

        private async void CountdownCompleted()
        {
            Console.WriteLine( "Countdown has completed." );
            await _tcpServer.SendMessage( "CountdownComplete" );
        }

        private void MessageSent( string message )
        {
            Console.WriteLine( $"Message sent to Unity: {message}" );
        }

        private void Window_Closed( object sender, EventArgs e )
        {
            _tcpServer.Disconnect();
        }
    }
}
