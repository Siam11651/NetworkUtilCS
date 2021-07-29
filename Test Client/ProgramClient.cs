using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using NetworkUtilCS;

namespace Client
{
    class Client
    { 
        public Client()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 44444);
            Socket clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.Connect(ipEndPoint);

            NetworkUtil networkUtil = new NetworkUtil(clientSocket);

            while (true)
            {
                try
                {
                    string str = Console.ReadLine();

                    networkUtil.Write(str);
                }
                catch(Exception exception)
                {
                    Console.WriteLine("Exception:");
                    Console.WriteLine(exception.Message);

                    break;
                }
            }

            clientSocket.Close();
        }
    }

    class ProgramClient
    {
        static void Main(string[] args)
        {
            Client client = new Client();
        }
    }
}
