using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

namespace ChatRoomServer
{
    class Program
    {
        //This is the server and in this main class we cofigurate all parameters to comunicate with clients
        static void Main(string[] args)
        {
            Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket conexion;
            IPEndPoint connect = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6400);

            listen.Bind(connect);
            listen.Listen(10);

            conexion = listen.Accept();
            Console.WriteLine("Conexion Exitosa");

            byte[] recieve_info = new byte[100];
            string data = "";
            int array_size = 0;

            array_size = conexion.Receive(recieve_info, 0, recieve_info.Length, 0);
            Array.Resize(ref recieve_info, array_size);

            data = Encoding.Default.GetString(recieve_info);

            Console.WriteLine("mensaje: {0}", data);
            Console.ReadKey();
        }
    }
}
