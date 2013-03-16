using System;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;


namespace IsolationServer
{
    internal class Program
    {
        private static TcpListener _listener;
        private const int LIMIT = 2; //5 concurrent clients

        private static Game _currentGame;

        public static void Main()
        {
            _listener = new TcpListener(2055); //2130706433 is 127.0.0.1
            _listener.Start();
            Console.WriteLine("Server mounted, listening to port 2055");
            _currentGame = new Game();
            Console.WriteLine("Created game with no players");
            for (int i = 0; i < LIMIT; i++)
            {
                var t = new Thread(Service);
                t.Start();
            }
        }

        public static void Service()
        {
            while (true)
            {
                Socket soc = _listener.AcceptSocket();
                //soc.SetSocketOption(SocketOptionLevel.Socket,
                //        SocketOptionName.ReceiveTimeout,10000);
                Console.WriteLine("Connected: {0}", soc.RemoteEndPoint);
                try
                {
                    Console.WriteLine("Player connected to game");
                    Stream s = new NetworkStream(soc);
                    var sr = new StreamReader(s);
                    var sw = new StreamWriter(s);
                    sw.AutoFlush = true; // enable automatic flushing
                    var player = _currentGame.AddPlayer(sw);
                    sw.WriteLine("Welcome " + (player ? "Player 1" : "Player 2"));
                    while (true)
                    {
                        var board = sr.ReadLine();
                        if (String.IsNullOrWhiteSpace(board)) break;
                        Console.WriteLine(player ? "Player 1" : "Player 2" + " plays: ");
                        Game.Display(board);
                        _currentGame.PlayerMove(player, board);
                    }
                    s.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Disconnected: {0}", soc.RemoteEndPoint);
                soc.Close();
            }
        }
    }
}