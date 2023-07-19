using System.Net;
using System.Net.Sockets;

namespace Server;

internal class Program 
{
    static void Main(string[] args) 
    {
        // param: 주소체계, 소켓타입
        // Stream 은 연결 지향을 의미
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //엔드 포인트
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.30.1"), 20000);
        
        //serverSocket에 ip와 port 할당
        serverSocket.Bind(endPoint);

        // Listen의 역할
        // 1. 클라이언트들의 연결 요청을 대기상태로 변경
        // 2. 백로그큐 생성 => 클라이언트들의 연결 요청 대기실
        //   최대 20개의 클라이언트 연결요청을 대기할 수 있다.
        serverSocket.Listen(20);


    }
}