using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TTTRGamePage : Page
    {
        ConnectionSockets cs2;
        int[,] asdf = { { 1, 1 }, { 2, 2 } };
        Button[,] btns = new Button[3,3];
        //Button[,] btns = { { B00, B01, B02 }, { B00, B01, B02 }, { B00, B01, B02 } };
        bool[,] clickable = new bool[3, 3];
        string mark;
        int player;
        bool canPickPlayer = true;
        bool[] isMyTurn = new bool[1];

        public  TTTRGamePage()
        {
            init();
            //for(int i = 0; i < clickable.GetLength(0); i++)
            //{
            //    for (int j = 0; j < clickable.GetLength(1); j++)
            //    {
            //        clickable[i, j] = true;
            //    }
            //}
            //this.InitializeComponent();
            //Button[,] btns1 = { { B00, B01, B02 }, { B10, B11, B12 }, { B20, B21, B22 } };
            //btns = btns1;
            //if (true)//make so can be server/p2 or client p1
            //{
            //    //cs2 = new ConnectionS2();
            //    isMyTurn[0] = true;
            //    cs2 = new ConnectionSockets(TextBox1, TextBox2, Block1, Block2, btns, clickable, this.Dispatcher, isMyTurn);
            //    mark = "X";
            //    player = 1;

            //    //init();
            //}


        }

        public async Task init()
        {
            for (int i = 0; i < clickable.GetLength(0); i++)
            {
                for (int j = 0; j < clickable.GetLength(1); j++)
                {
                    clickable[i, j] = true;
                }
            }
            this.InitializeComponent();
            Button[,] btns1 = { { B00, B01, B02 }, { B10, B11, B12 }, { B20, B21, B22 } };
            btns = btns1;
        }

        private void OpponentMove(int[] movePos)
        {

            clickable[movePos[0], movePos[1]] = false;
            //if(movePos[0] == 0 && movePos[1] == 0)
            //{
            //    clickable[0, 0] = false;
            //}
            //else if (movePos[0] == 0 && movePos[1] == 1)
            //{
            //    clickable[0, 1] = false;
            //}
            //else if (movePos[0] == 0 && movePos[1] == 2)
            //{
            //    clickable[0, 2] = false;
            //}
            //else if (movePos[0] == 1 && movePos[1] == 0)
            //{
            //    clickable[1, 0] = false;
            //}
            //else if (movePos[0] == 1 && movePos[1] == 1)
            //{
            //    clickable[1, 1] = false;
            //}
            //else if (movePos[0] == 1 && movePos[1] == 2)
            //{
            //    clickable[1, 2] = false;
            //}
            //else if (movePos[0] == 2 && movePos[1] == 0)
            //{
            //    clickable[2, 0] = false;
            //}
            //else if (movePos[0] == 2 && movePos[1] == 1)
            //{
            //    clickable[2, 1] = false;
            //}
            //else if (movePos[0] == 2 && movePos[1] == 2)
            //{
            //    clickable[2, 2] = false;
            //}
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)//[0,0]
        {
            int thisRow = 0;
            int thisCol = 0;

            MakeMove(thisRow, thisCol);

            // if (clickable[0, 0])
            // {
            //string move = "0 0";
            //int[] movePos = new int[2];
            //cs2.SendMessage(move);
            //B00.Content = cs2.game.board[0, 0];
            // cs2.ReceiveMessage(movePos);
            //clickable[0, 0] = false;
            //clickable[movePos[0], movePos[1]] = false;
            ////call display opponent move
            //OpponentMove(movePos);
            ////call check win and display if won
            //if(cs2.game.WhoWon() == cs2.game.player)
            //{
            //    this.Frame.Navigate(typeof(WinPage));
            //}
            //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
            //{
            //    this.Frame.Navigate(typeof(LosePage));
            //}
            //}
            // B00.Content = "X";
        }

        private  void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int thisRow = 0;
            int thisCol = 1;

            MakeMove(thisRow, thisCol);


            if (clickable[0, 1])
            {
                //string move = "0 1";
                //int[] movePos = new int[2];
                //cs2.SendMessage(move);
                //B01.Content = cs2.game.board[0, 1];
                // cs2.ReceiveMessage(movePos);
                //clickable[0, 1] = false;
                //clickable[movePos[0], movePos[1]] = false;
                //OpponentMove(movePos);
                //if (cs2.game.WhoWon() == cs2.game.player)
                //{
                //    this.Frame.Navigate(typeof(WinPage));
                //}
                //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                //{
                //    this.Frame.Navigate(typeof(LosePage));
                //}
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int thisRow = 0;
            int thisCol = 2;

            MakeMove(thisRow, thisCol);

            //B02.Content = "X";
            if (clickable[0, 2])
            {
                //string move = "0 2";
                //int[] movePos = new int[2];
                //cs2.SendMessage(move);
                //B02.Content = cs2.game.board[0, 2];
                ////await cs2.ReceiveMessage(movePos);
                // cs2.ReceiveMessage(movePos);
                //clickable[0, 2] = false;
                //clickable[movePos[0], movePos[1]] = false;
                //OpponentMove(movePos);
                //if (cs2.game.WhoWon() == cs2.game.player)
                //{
                //    this.Frame.Navigate(typeof(WinPage));
                //}
                //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                //{
                //    this.Frame.Navigate(typeof(LosePage));
                //}
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int thisRow = 1;
            int thisCol = 0;

            MakeMove(thisRow, thisCol);


            //B10.Content = "X";
            if (clickable[1, 0])
            {
                //string move = "1 0";
                //int[] movePos = new int[2];
                //cs2.SendMessage(move);
                //B10.Content = cs2.game.board[1, 0];
                // cs2.ReceiveMessage(movePos);
                //clickable[1, 0] = false;
                //clickable[movePos[0], movePos[1]] = false;
                //OpponentMove(movePos);
                //if (cs2.game.WhoWon() == cs2.game.player)
                //{
                //    this.Frame.Navigate(typeof(WinPage));
                //}
                //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                //{
                //    this.Frame.Navigate(typeof(LosePage));
                //}
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int thisRow = 1;
            int thisCol = 1;

            MakeMove(thisRow, thisCol);

            if (clickable[1, 1])
            {
                //string move = "1 1";
                //int[] movePos = new int[2];
                //cs2.SendMessage(move);
                //B11.Content = cs2.game.board[1, 1];
                // cs2.ReceiveMessage(movePos);
                //clickable[1, 1] = false;
                //clickable[movePos[0], movePos[1]] = false;
                //OpponentMove(movePos);
                //if (cs2.game.WhoWon() == cs2.game.player)
                //{
                //    this.Frame.Navigate(typeof(WinPage));
                //}
                //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                //{
                //    this.Frame.Navigate(typeof(LosePage));
                //}
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            int thisRow = 1;
            int thisCol = 2;

            MakeMove(thisRow, thisCol);

            if (clickable[1, 2])
            {
                //string move = "1 2";
                //int[] movePos = new int[2];
                //cs2.SendMessage(move);
                //B12.Content = cs2.game.board[1, 2];
                // cs2.ReceiveMessage(movePos);
                //clickable[1, 2] = false;
                //clickable[movePos[0], movePos[1]] = false;
                //OpponentMove(movePos);
                //if (cs2.game.WhoWon() == cs2.game.player)
                //{
                //    this.Frame.Navigate(typeof(WinPage));
                //}
                //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                //{
                //    this.Frame.Navigate(typeof(LosePage));
                //}
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            int thisRow = 2;
            int thisCol = 0;

            MakeMove(thisRow, thisCol);

            if (clickable[2, 0])
            {
                //string move = "2 0";
                //int[] movePos = new int[2];
                //cs2.SendMessage(move);
                //B20.Content = cs2.game.board[2, 0];
                // cs2.ReceiveMessage(movePos);
                //clickable[2, 0] = false;
                //clickable[movePos[0], movePos[1]] = false;
                //OpponentMove(movePos);
                //if (cs2.game.WhoWon() == cs2.game.player)
                //{
                //    this.Frame.Navigate(typeof(WinPage));
                //}
                //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                //{
                //    this.Frame.Navigate(typeof(LosePage));
                //}
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            int thisRow = 2;
            int thisCol = 1;

            MakeMove(thisRow, thisCol);

            if (clickable[2, 1])
            {
                //string move = "2 1";
                //int[] movePos = new int[2];
                //cs2.SendMessage(move);
                //B21.Content = cs2.game.board[2, 1];
                // cs2.ReceiveMessage(movePos);
                //clickable[2, 1] = false;
                //clickable[movePos[0], movePos[1]] = false;
                //OpponentMove(movePos);
                //if (cs2.game.WhoWon() == cs2.game.player)
                //{
                //    this.Frame.Navigate(typeof(WinPage));
                //}
                //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                //{
                //    this.Frame.Navigate(typeof(LosePage));
                //}
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            int thisRow = 2;
            int thisCol = 2;
            MakeMove(thisRow, thisCol);

            if (clickable[2, 2])
            {
                //string move = "2 2";
                //int[] movePos = new int[2];
                //cs2.SendMessage(move);
                //B22.Content = cs2.game.board[2, 2];
                // cs2.ReceiveMessage(movePos);
                //clickable[2, 2] = false;
                //clickable[movePos[0], movePos[1]] = false;
                //OpponentMove(movePos);
                //if (cs2.game.WhoWon() == cs2.game.player)
                //{
                //    this.Frame.Navigate(typeof(WinPage));
                //}
                //else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                //{
                //    this.Frame.Navigate(typeof(LosePage));
                //}
            }
        }

        private async void MakeMove(int thisRow, int thisCol)
        {

            if (clickable[thisRow, thisCol] && isMyTurn[0])
            {
                isMyTurn[0] = false;
                clickable[thisRow, thisCol] = false;
                cs2.game.MakeMyMove(thisRow, thisCol);
                //btns[thisRow, thisCol].Content = mark;
                UpdateBoard();
                if (player == 1)
                {
                    cs2.StartClient("D" + thisRow.ToString() + thisCol.ToString());
                    //btns[thisRow, thisCol].Content += "???";
                    //btns[thisRow, thisCol].Content += "???" + cs2.game.player.ToString() + "     ";
                }
                else if (player == 2)
                {
                    cs2.StartClient2("D" + thisRow.ToString() + thisCol.ToString());
                    //btns[thisRow, thisCol].Content += "$$$";
                }
                //call check win and display if won
                if (cs2.game.WhoWon() == cs2.game.player)
                {
                    //btns[thisRow, thisCol].Content += cs2.game.WhoWon().ToString()+ cs2.game.player.ToString() + cs2.game.mark;
                    //await Task.Delay(5000);
                    cs2.CloseServer();
                    this.Frame.Navigate(typeof(WinPage));
                }
                else if (cs2.game.WhoWon() == cs2.game.otherPlayer)
                {
                    cs2.CloseServer();
                    this.Frame.Navigate(typeof(LosePage));
                }

            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            //Block1.Text = "just testing";
            cs2.StartClient("M" + TextBox1.Text);
            TextBox1.Text = "";
            //Block1.Text += " still testing";
        }

        private void SendBtn2_Click(object sender, RoutedEventArgs e)
        {
            
            cs2.StartClient2("M" + TextBox2.Text);
            TextBox2.Text = "";
        }

        private void Player1Btn_Click(object sender, RoutedEventArgs e)
        {
            if (canPickPlayer)//make so can be server/p2 or client p1
            {
                player = 1;
                canPickPlayer = false;
                isMyTurn[0] = true;
                mark = "X";
                cs2 = new ConnectionSockets(TextBox1, TextBox2, Block1, Block2, btns, clickable, this.Dispatcher, isMyTurn, player, mark, this);
                
                Player1Btn.Content = "Player 1 | X";
                Player1Btn.Background = new SolidColorBrush(Color.FromArgb(255, 48, 179, 221));
                Player2Btn.Content = "";
                init();
            }
        }

        private void Player2Btn_Click(object sender, RoutedEventArgs e)
        {
            if (canPickPlayer)//make so can be server/p2 or client p1
            {
                player = 2;
                canPickPlayer = false;
                isMyTurn[0] = false;
                mark = "O";
                cs2 = new ConnectionSockets(TextBox1, TextBox2, Block1, Block2, btns, clickable, this.Dispatcher, isMyTurn, player, mark, this);


                Player2Btn.Content = "Player 2 | O";
                Player2Btn.Background = new SolidColorBrush(Color.FromArgb(255, 48, 179, 221));
                Player1Btn.Content = "";
                init();
            }
        }

        public void UpdateBoard()
        {
            for(int i = 0; i < cs2.game.board.GetLength(0); i++)
            {
                for (int j = 0; j < cs2.game.board.GetLength(1); j++)
                {
                    btns[i, j].Content = cs2.game.board[i, j];
                }
            }
        }
    }
}


