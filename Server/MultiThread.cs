using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class MultiThread
    {
        public static void Run() {

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.31.1"), 17841);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(1000);

            while (true) 
            {
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine(clientSocket.RemoteEndPoint);

                // 다음 클라이언트 소켓을 받으려면 Accept를 호출해야 되는데
                // 아래 while문이 끝나기 전까지는 다음 accept 함수를 호출할 수 없다. 
                while (true) {
                    byte[] buffer = new byte[256];
                    int n1 = clientSocket.Receive(buffer);
                    if (n1 < 1) {
                        clientSocket.Dispose();
                        break;
                    }

                }                      
            }            
        }

        public static void RunWithThread()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.31.1"), 17841);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(1000);

            //while문을 thread에 할당해보자. 
            while (true) {
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine(clientSocket.RemoteEndPoint);
                // Receive와 Accept가 쓰레드가 다르므로 동시에 실행
                Thread t1 = new Thread(() => {
                    while (true) {
                        byte[] buffer = new byte[256];
                        int n1 = clientSocket.Receive(buffer);
                        if (n1 < 1) {
                            clientSocket.Dispose();
                            break;
                        }

                    }
                });

                t1.Start();
            }
        }

    }
}
