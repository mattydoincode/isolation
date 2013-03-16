using System;
using System.IO;
using System.Net.Sockets;

namespace IsolationClient
{
    internal class IsolationClient
    {
        public static void Main(string[] args)
        {
            var client = new TcpClient(args[0], 2055); //It takes in an IP?
            try
            {
                var s = client.GetStream();
                var sr = new StreamReader(s);
                var sw = new StreamWriter(s);
                sw.AutoFlush = true;
                var initialMessage = sr.ReadLine();
                Console.WriteLine();
                Console.WriteLine();
                while (true)
                {
                    Console.WriteLine(sr.ReadLine());//ACCEPT THE OPPONENT'S BOARD HERE YO
                    Console.WriteLine("type a board: "); //JUST TO TEST
                    string board = Console.ReadLine(); //  ADD YOUR MOVE HERE YO
                    sw.WriteLine(board);
                    if (board == "") break;
                }
                s.Close();
            }
            finally
            {
                // code in finally block is guranteed 
                // to execute irrespective of 
                // whether any exception occurs or does 
                // not occur in the try block
                client.Close();
            }
        }
    }
}