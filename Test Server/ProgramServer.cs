using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetworkUtilCS;

namespace Server
{
    class Server
    {
        private List<Socket> clientSockets;

        public Server()
        {
            clientSockets = new List<Socket>();
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 44444);
            Socket serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(10);

            Thread writeThread = new Thread(new ThreadStart(()=>
            {
                while(true)
                {
                    Socket clientSocket;

                    try
                    {
                        Console.WriteLine("Waiting for connection...");
                        clientSocket = serverSocket.Accept();
                        clientSockets.Add(clientSocket);
                    }
                    catch(Exception exception)
                    {
                        Console.WriteLine("Exception:");
                        Console.WriteLine(exception.Message);
                    }
                }
            }));

            writeThread.Start();

            while(true)
            {
                string str = Console.ReadLine();

                try
                {
                    foreach (Socket clientSocket in clientSockets)
                    {
                        NetworkUtil networkUtil = new NetworkUtil(clientSocket);

                        networkUtil.Write(str);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Exception:");
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }

    class ProgramServer
    {
        static void Main(string[] args)
        {
            Server server = new Server();
        }
    }
}
