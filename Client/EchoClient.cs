using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class EchoClient
    {
        public static void CreateEchoClient() {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.30.1"), 20000);
                socket.Connect(endPoint);
      
                while (true) {
                    //클라이언트에서 입력받은 메시지를 서버로 전송
                    string str = Console.ReadLine();
                    // exit 라는 메시지를 받으면 프로그램 종료
                    if (str == "exit") {
                        return;
                    }

                    // 메시지를 바이트 배열로 직렬화
                    byte[] buffer = Encoding.UTF8.GetBytes(str);

                    //서버에 데이터 전송
                    socket.Send(buffer);

                    byte[] buffer2 = new byte[1024];
                    int bytesRead = socket.Receive(buffer2);

                    //1바이트 미만을 받으면 서버가 접속종료한 것으로 판단
                    if (bytesRead < 1) {
                        Console.WriteLine("서버 연결 종료");
                    }

                    string str2 = Encoding.UTF8.GetString(buffer2);
                    Console.WriteLine("보낸 메시지: " + str2);
                }
            }
        }

    }
}
