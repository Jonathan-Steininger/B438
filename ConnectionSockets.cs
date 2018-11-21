using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace App1
{
    public class ConnectionSockets
    {
        //bool isServer = false;
        //bool isClient = false;
        //HostName hostName = new HostName("localhost");
        HostName hostName = new HostName("192.168.0.15");
        static string PortNumber = "1339";
        static string PortNumber2 = "1337";
        TextBox tb1;
        TextBox tb2;
        TextBlock b1;
        TextBlock b2;
        Button[,] btns;
        bool[,] clickable;
        CoreDispatcher Dispatcher;
        bool[] isMyTurn;
        string mark;

        public TTTR game;
        TTTRGamePage gamePage;

        Stream outputStream;
        StreamSocket streamSocket;
        StreamSocketListener streamSocketListener1;
        StreamSocketListener streamSocketListener2;
        StreamWriter streamWriter;
        StreamReader streamReader;

        public ConnectionSockets(TextBox tb1, TextBox tb2, TextBlock b1, TextBlock b2, Button[,] btns, bool[,] clickable, CoreDispatcher dispatcher, bool[] isMyTurn, int playerNumber, string mark, TTTRGamePage gamePage)
        {
            this.StartServer();
            this.StartServer2();
            this.tb1 = tb1;
            this.tb2 = tb2;
            this.b1 = b1;
            this.b2 = b2;
            this.btns = btns;
            this.clickable = clickable;
            this.Dispatcher = dispatcher;
            this.isMyTurn = isMyTurn;
            this.mark = mark;

            game = new TTTR(playerNumber, mark);
            this.gamePage = gamePage;
        }

        public void CloseServer()
        {
            this.streamSocketListener1.Dispose();
            this.streamSocketListener2.Dispose();
        }

        public async void StartServer()
        {
            try
            {
                var streamSocketListener = new StreamSocketListener();
                this.streamSocketListener1 = streamSocketListener;
                // The ConnectionReceived event is raised when connections are received.
                streamSocketListener.ConnectionReceived += this.StreamSocketListener_ConnectionReceived;

                // Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
                await streamSocketListener.BindServiceNameAsync(PortNumber);

                // this.serverListBox.Items.Add("server is listening 1...");
            }
            catch (Exception ex)
            {
                SocketErrorStatus webErrorStatus = SocketError.GetStatus(ex.GetBaseException().HResult);
                //this.serverListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
            }
        }

        public async void StreamSocketListener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            string request;
            using (var streamReader = new StreamReader(args.Socket.InputStream.AsStreamForRead()))
            {
                request = await streamReader.ReadLineAsync();
            }

            if (request.StartsWith("M"))
            {
                request = request.Substring(1);
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.b2.Text += request + " sending 2 from 1 \n");
            }
            else
            {
                request = request.Substring(1);
                string pos1, pos2, mark;
                int row, col;
                pos1 = request.Substring(0, 1);
                pos2 = request.Substring(1, 1);
                mark = request.Substring(2);
                row = int.Parse(pos1);
                col = int.Parse(pos2);
                game.MakeOpponentMove(row, col);

                isMyTurn[0] = true;
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => gamePage.UpdateBoard());
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.clickable[row, col] = false);
                if (game.WhoWon() == game.player)
                {
                    sender.Dispose();
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => gamePage.Frame.Navigate(typeof(WinPage), null, new DrillInNavigationTransitionInfo()));
                }
                else if (game.WhoWon() == game.otherPlayer)
                {
                    sender.Dispose();
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => gamePage.Frame.Navigate(typeof(LosePage), null, new DrillInNavigationTransitionInfo()));
                }
            }


        }

        public async void StartClient(string message)
        {
            try
            {
                // Create the StreamSocket and establish a connection to the echo server.

                //using (var streamSocket = new StreamSocket())
                streamSocket = new StreamSocket();
                //{
                // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
                //var hostName = new Windows.Networking.HostName("192.168.0.12");
                //HostName hostName = new HostName("localhost");

                // this.clientListBox.Items.Add("client is trying to connect...");

                await streamSocket.ConnectAsync(hostName, PortNumber);

                using (Stream outputStream = streamSocket.OutputStream.AsStreamForWrite())
                {
                    using (var streamWriter = new StreamWriter(outputStream))
                    {
                        await streamWriter.WriteLineAsync(message);
                        await streamWriter.FlushAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                SocketErrorStatus webErrorStatus = SocketError.GetStatus(ex.GetBaseException().HResult);
                //this.clientListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
            }
        }

        public async void SendChatBtn_Click(object sender, RoutedEventArgs e)
        {
            //await this.StartClient(ChatBox.Text + " ?????");
            //this.SendFromClientAsync(ChatBox.Text);
            //ChatBox.Text = "";

            //chatDialog.Text += ChatBox.Text + "\n\n";
        }

        public void CloseConnection_Click(object sender, RoutedEventArgs e)
        {

        }

        public async void SendChatBtn2_Click(object sender, RoutedEventArgs e)
        {

            // await this.StartClient2(TextBox2.Text + " IM HERE");
            //TextBox2.Text = "";
        }



        ////////////////////////////////////////////////////

        public async void StartServer2()
        {
            try
            {
                var streamSocketListener = new StreamSocketListener();
                this.streamSocketListener2 = streamSocketListener;
                // The ConnectionReceived event is raised when connections are received.
                streamSocketListener.ConnectionReceived += this.StreamSocketListener_ConnectionReceived2;

                // Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
                await streamSocketListener.BindServiceNameAsync(PortNumber2);

                //this.serverListBox.Items.Add("server is listening 2 ...");
            }
            catch (Exception ex)
            {
                SocketErrorStatus webErrorStatus = SocketError.GetStatus(ex.GetBaseException().HResult);
                //this.serverListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
            }
        }

        public async void StreamSocketListener_ConnectionReceived2(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            string request;
            using (var streamReader = new StreamReader(args.Socket.InputStream.AsStreamForRead()))
            {
                request = await streamReader.ReadLineAsync();
            }
            if (request.StartsWith("M"))
            {
                request = request.Substring(1);
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.b1.Text += request + " sending 1 from 2 \n");
            }
            else
            {
                request = request.Substring(1);
                string pos1, pos2, mark;
                int row, col;
                pos1 = request.Substring(0, 1);
                pos2 = request.Substring(1, 1);
                mark = request.Substring(2);
                row = int.Parse(pos1);
                col = int.Parse(pos2);
                game.MakeOpponentMove(row, col);


                isMyTurn[0] = true;
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => gamePage.UpdateBoard());
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.clickable[row, col] = false);
                if (game.WhoWon() == game.player)
                {
                    sender.Dispose();
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => gamePage.Frame.Navigate(typeof(WinPage), null, new DrillInNavigationTransitionInfo()));
                }
                else if (game.WhoWon() == game.otherPlayer)
                {
                    sender.Dispose();
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => gamePage.Frame.Navigate(typeof(LosePage), null, new DrillInNavigationTransitionInfo()));
                }
            }
        }



        public async void StartClient2(string message)
        {
            try
            {
                // Create the StreamSocket and establish a connection to the echo server.
                using (var streamSocket = new StreamSocket())
                {
                    // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
                    //var hostName = new Windows.Networking.HostName("192.168.0.12");
                    //HostName hostName = new HostName("localhost");

                    await streamSocket.ConnectAsync(hostName, PortNumber2);
                    using (Stream outputStream = streamSocket.OutputStream.AsStreamForWrite())
                    {
                        using (var streamWriter = new StreamWriter(outputStream))
                        {
                            await streamWriter.WriteLineAsync(message);
                            await streamWriter.FlushAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SocketErrorStatus webErrorStatus = SocketError.GetStatus(ex.GetBaseException().HResult);
                //this.clientListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
            }
        }
    }
}









