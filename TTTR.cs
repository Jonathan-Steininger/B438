using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{

    public class TTTR
    {
        public bool isPlaying = false;
        public string[,] board = new string[3, 3];
        public int player;
        public int otherPlayer = 1;
        public string mark = "O";
        public string otherMark = "X";
        int turnCount;
        public TTTR(int player, string mark)
        {
            turnCount = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "  ";
                }
            }
            isPlaying = true;
            this.player = player;
            if (this.player == 1)
            {
                otherPlayer = 2;
                //this.mark = "X";
                //this.otherMark = "O";
            }
            this.mark = mark;
            if (this.mark.Equals("X"))
            {
                this.otherMark = "O";
            }
        }

        public void DisplayBoard()
        {
            string message = "";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    message += board[i, j] + " | ";
                    //Console.Write(board[i, j] + " | ");
                }
                //message = message.Substring(0, message.Length - 2);
                //Console.WriteLine(message);
                //message = "";
                //Console.WriteLine("----------------");
            }
        }
        public bool CheckWin()
        {
            if (WhoWon() == player)
            {
                //Console.WriteLine("You Won!");
                return true;
            }
            else if (WhoWon() == otherPlayer)
            {
                //Console.WriteLine("You Lost!");
                return true;
            }
            return false;
        }




        public void MakeMyMove(int row, int col)
        {
            turnCount++;
            board[row, col] = mark;
        }

        public void MakeOpponentMove(int row, int col)
        {
            turnCount++;
            board[row, col] = otherMark;
        }

        public int WhoWon()
        {
            if (turnCount < 5)
            {
                return 0;
            }
            isPlaying = false;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (WonRow(mark, i) || WonCol(mark, i))
                {
                    return player;
                }
                if (WonRow(otherMark, i) || WonCol(otherMark, i))
                {
                    return otherPlayer;
                }
            }

            if (WonDiags(mark))
            {
                return player;
            }
            if (WonDiags(otherMark))
            {
                return otherPlayer;
            }

            isPlaying = true;
            return 0;
        }

        public bool WonRow(string s, int row)
        {
            return (board[row, 0].Equals(s)) && (board[row, 0].Equals(board[row, 1]) && board[row, 0].Equals(board[row, 2]));
        }

        public bool WonCol(string s, int col)
        {
            return board[0, col].Equals(s) && (board[0, col].Equals(board[1, col]) && board[0, col].Equals(board[2, col]));
        }

        public bool WonDiags(string s)
        {
            return (board[0, 0].Equals(s) && (board[0, 0].Equals(board[1, 1]) && board[0, 0].Equals(board[2, 2]))) || (board[0,2].Equals(s) && (board[0, 2].Equals(board[1, 1]) && board[0, 2].Equals(board[2, 0])));
        }
    }
}
