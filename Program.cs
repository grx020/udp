using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:12121/api/variables/getvalue?varName=engineRPM";
            string apiKey = "your_api_key_here";
            string authHeader = "Bearer " + apiKey;

            // 定义本地UDP监听端口
            int localPort = 12345;

            // 创建UDP监听器
            UdpClient listener = new UdpClient(localPort);

            Console.WriteLine($"Listening on port {localPort}...");

            // 接收UDP数据包
            while (true)
            {
                try
                {
                    // 阻塞等待UDP数据包到达
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = listener.Receive(ref remoteEndPoint);

                    // 将字节数组转换成字符串
                    string message = Encoding.UTF8.GetString(data);

                    // 输出收到的数据包内容和发送者信息
                    Console.WriteLine($"Received: {message} from {remoteEndPoint}");

                    // 回复数据包
                    byte[] replyMessage = Encoding.UTF8.GetBytes("Hello, ESP32!");
                    listener.Send(replyMessage, replyMessage.Length, remoteEndPoint);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
