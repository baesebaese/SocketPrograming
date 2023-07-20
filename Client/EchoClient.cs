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
                    byte[] strBuffer = Encoding.UTF8.GetBytes(str);

                    //데이터의 길이를 나타내주는 2바이트의 헤더 추가
                    byte[] newBuffer = new byte[2 + strBuffer.Length];
                    byte[] dataSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)strBuffer.Length));

                    Array.Copy(dataSize, 0, newBuffer, 0, dataSize.Length);

                    Array.Copy(strBuffer, 0, newBuffer, 2, strBuffer.Length);
                    //서버에 데이터 전송
                    socket.Send(newBuffer);
                }
            }
        }
    }
}
