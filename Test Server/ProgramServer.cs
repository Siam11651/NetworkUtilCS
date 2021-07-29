using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetworkUtilCS;

namespace Server
{
    class Server
    {

        public Server()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 44444);
            Socket serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(10);

            while(true)
            {
                Console.WriteLine("Waiting for connection...");

                Socket clientSocket = serverSocket.Accept(); ;

                Thread readThread = new Thread(new ThreadStart(() =>
                {
                    NetworkUtil networkUtil = new NetworkUtil(clientSocket);

                    while (true)
                    {
                        object o = networkUtil.Read();

                        if (o.GetType() == typeof(string))
                        {
                            string str = (string)o;

                            Console.WriteLine(str);
                        }
                    }
                }));

                readThread.Start();
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
