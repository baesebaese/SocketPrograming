using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class ServerSocket
    {
        public static void CreateServerSocket() {

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

            // 3. 대기실에서 연결 요청중인 클라이언트 중 하나에 연결요청을 수락
            // 클라이언트와 데이터 통신을 위한 소켓을 만든다.
            Socket clientSocket = serverSocket.Accept();

            // 클라이언트 ip와 port 확인
            Console.WriteLine("연결성공 : " + clientSocket.RemoteEndPoint);

            Console.WriteLine();
            Console.WriteLine();

            //리틀엔디언
            byte[] buffer = BitConverter.GetBytes(1234567);
            Console.WriteLine(BitConverter.ToString(buffer));

            // 빅엔디언
            int num = IPAddress.HostToNetworkOrder(1234567);

            byte[] buffer2 = BitConverter.GetBytes(num);
            Console.WriteLine(BitConverter.ToString(buffer2));
        }
    }
}
