using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsolationServer
{
    public class Game
    {
        public int Players { get; set; }

        public StreamWriter Player1 { get; set; }
        public StreamWriter Player2 { get; set; }

        public void Reset()
        {
            Players = 0;
        }

        public bool AddPlayer(StreamWriter sw)
        {
            if (Player1 == null)
            {
                Player1 = sw;
                return true;
            }
            else
            {
                Player2 = sw;
                return false;
            }
        }

        public void PlayerMove(bool player1, string board)
        {
            if (player1)
            {
                Player2.WriteLine(board);
            }
            else
            {
                Player1.WriteLine(board);
            }
            
        }
        public static void Display(string board)
        {
            Console.WriteLine("shit man");
        }

    }
}
