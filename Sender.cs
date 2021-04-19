using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameMapTestClient
{
    class Sender
    {
        static void Main(string[] args)
        {
            Connect("localhost", "test");
        }

        static void Connect(String server, String message)
        {
            try
            {
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sent: {message}");

                data = new Byte[256];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);

                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine($"Received: {responseData}");

                stream.Close();
                client.Close();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Please specify a valid Server: " + e);
            }
            catch ( SocketException e)
            {
                Console.WriteLine("Socket Exception: " + e);
            }
            Console.Write("Acknowledge?");
            Console.Read();
        }

    }
}
