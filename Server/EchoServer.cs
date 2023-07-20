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
                        byte[] headerBuffer = new byte[2];
                        int n1 = clientSocket.Receive(headerBuffer);

                        //반환값이 없으면 클라이언트가 연결을 종료했다고 판단
                        if (n1 < 1) {
                            Console.WriteLine("클라이언트 연결 종료");
                            return;
                        }
                        else if (n1 == 1) {
                            clientSocket.Receive(headerBuffer, 1, 1, SocketFlags.None);
                        }

                        short dataSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(headerBuffer));
                        byte[] dataBuffer = new byte[dataSize];

                        int totalRecv = 0;

                        while (totalRecv < dataSize) {
                            int n2 = clientSocket.Receive(dataBuffer, totalRecv, dataSize - totalRecv, SocketFlags.None);
                            totalRecv += n2;

                        }

                        //받은 데이터 역직렬화
                        string str = Encoding.UTF8.GetString(dataBuffer);
                        Console.WriteLine("받은 메시지: " + str);
                    }
                }
                
            }       
        }
    }
}
