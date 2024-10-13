using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Unity3DDemo
{
    public class TcpServer
    {
        private TcpListener _server;
        private TcpClient _client;
        private NetworkStream _stream;

        public event Action OnConnect;
        public event Action OnDisconnect;
        public event Action<string> OnMessageReceived;
        public event Action<string> OnMessageSend;

        public async Task StartListeningAsync( int port )
        {
            _server = new TcpListener( IPAddress.Any, port );
            _server.Start();
            _client = await _server.AcceptTcpClientAsync();
            OnConnect?.Invoke(); // Trigger OnConnect event
            _stream = _client.GetStream();
            ListenForMessages();
        }

        private async void ListenForMessages()
        {
            byte[] buffer = new byte[1024];
            while ( _client.Connected )
            {
                try
                {
                    int byteCount = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if ( byteCount > 0 )
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                        OnMessageReceived?.Invoke( message ); // Trigger OnMessageReceived event
                    }
                }
                catch ( Exception )
                {
                    Disconnect(); // Handle client disconnection gracefully
                }
            }
        }

        public async Task SendMessage( string message )
        {
            if ( _stream != null && _stream.CanWrite )
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                await _stream.WriteAsync( data, 0, data.Length );
                OnMessageSend?.Invoke( message ); // Trigger OnMessageSend event
            }
        }

        public void Disconnect()
        {
            OnDisconnect?.Invoke(); // Trigger OnDisconnect event before cleanup
            _stream?.Close();
            _client?.Close();
            _server?.Stop();
        }
    }
}