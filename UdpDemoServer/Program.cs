using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpDemoServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This is the port that the server will listen on.
            // You can change it here.
            const int listenPort = 54445;

            // Since UDP is connectionless, the server only needs to listen for
            // packets and react to them one at a time.

            // (If you were writing a TCP server, you'd listen for connection requests
            // and then instantiate a class that handles that specific connection, then go
            // back to listening for new connections.)

            // Yes, you use UdpClient in order to implement a UDP server. Since UDP is 
            // connectionless and is simply "one end sends a packet, other end receives
            // that packet", each end is, technically, both a client and a server.

            using (UdpClient udpServer = new UdpClient(listenPort))
            {

                Console.WriteLine($"Server is listening on port {listenPort}...");

                // Start infinite loop
                while (true)
                {
                    // This creates an IPEndpoint object, which is used by the UdpClient,
                    // to determine on which interface to listen for packets on. In this
                    // case, it listens on all interfaces (hence "Any").
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    // This will block until a packet is received.
                    // Once the packet is received, its payload will be in receivedBytes.
                    byte[] receivedBytes = udpServer.Receive(ref remoteEndPoint);

                    // This will decode the data received in the packet and print it on
                    // the console.
                    string receivedData = Encoding.UTF8.GetString(receivedBytes);
                    Console.WriteLine($"Received: {receivedData} from {remoteEndPoint}");

                    // If the packet contained exactly the string "EXIT" then the server
                    // will exit the loop (and not echo back the packet).
                    if (receivedData == "EXIT")
                    {
                        Console.WriteLine("Exiting...");
                        break;
                    }

                    // Send a packet back to the client, on the same port that the client
                    // originated from, that contains a copy of the payload.
                    udpServer.Send(receivedBytes, receivedBytes.Length, remoteEndPoint);
                    Console.WriteLine($"Echoed back to {remoteEndPoint}");

                    // The infinite loop will now continue.
                }
            }
            // Program stops execution here.
        }
    }
}
