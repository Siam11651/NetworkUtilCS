using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetworkUtilCS
{
    public class NetworkUtil
    {
        private Socket clientSocket;

        public NetworkUtil(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        public Socket GetSocket()
        {
            return clientSocket;
        }

        public void Write(object o)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            binaryFormatter.Serialize(memoryStream, o);

            byte[] objectSizeByteArray = BitConverter.GetBytes(memoryStream.GetBuffer().Length);

            clientSocket.Send(objectSizeByteArray);
            clientSocket.Send(memoryStream.GetBuffer());
        }

        public object Read()
        {
            byte[] objectSizeByteArr = new byte[sizeof(int)];

            clientSocket.Receive(objectSizeByteArr);

            int objectSize = BitConverter.ToInt32(objectSizeByteArr);
            byte[] objectByteArray = new byte[objectSize];

            clientSocket.Receive(objectByteArray);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            object o = binaryFormatter.Deserialize(new MemoryStream(objectByteArray));

            return o;
        }
    }
}
