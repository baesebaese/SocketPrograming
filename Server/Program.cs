using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

namespace Server;

internal class Program 
{
    static void Main(string[] args) 
    {
        // ServerSocket.CreateServerSocket();
        //EchoServer.CreateEchoServer();
        MultiThread.RunWithThread();
    }
}