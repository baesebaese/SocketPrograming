using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class EchoServer
    {
        public static void CreateEchoServer() 
        {

            using (Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.30.1"), 20000);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(20);

                using (Socket clientSocket = serverSocket.Accept()) {
                    Console.WriteLine(clientSocket.RemoteEndPoint);

                    while (true) {
                        byte[] buffer = new byte[1024];
                        // 클라이언트에서 전송한 데이터를 받는다. 
                        int totalBytes = clientSocket.Receive(buffer);

                        //반환값이 없으면 클라이언트가 연결을 종료했다고 판단
                        if (totalBytes < 1) {
                            Console.WriteLine("클라이언트 연결 종료");
                            return;
                        }

                        //받은 데이터 역직렬화
                        string str = Encoding.UTF8.GetString(buffer);
                        Console.WriteLine("받은 메시지: " + str);

                        //클라이언트에게 받은 메시지를 재전송
                        clientSocket.Send(buffer);
                    }
                }
            }       
        }
    }
}
