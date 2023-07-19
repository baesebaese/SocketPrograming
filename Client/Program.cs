using System.Net;
using System.Net.Sockets;

namespace Client;

internal class Program
{
    static void Main(string[] args)
    {
        // 1. 클라이언트 소켓 생성
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.30.1"), 20000);

        // 2. 커넥트하면 서버소켓의 백로그큐에 들어가서
        // 클라이언트가 연결요청을 대기하게 된다. 
        clientSocket.Connect(endPoint);

    }
}