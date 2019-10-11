using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatRoomServer
{
    class servidor
    {
        /*        
            TcpListener--------> Wait the conexion from client.        
            TcpClient----------> Offer the conexion beween server and client      
            NetworkStream------> Se encarga de enviar mensajes atravez de los sockets.        
        */

        private TcpListener server;
        private TcpClient client = new TcpClient();
        private IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Any, 8000);
        private List<Connection> list = new List<Connection>();

        Connection con;


        private struct Connection
        {
            public NetworkStream stream;
            public StreamWriter streamw;
            public StreamReader streamr;
            public string nick;
        }

        public servidor()
        {
            Inicio();
        }

        public void Inicio()
        {

            Console.WriteLine("Server OK!");
            server = new TcpListener(ipendpoint);
            server.Start();

            while (true)
            {
                client = server.AcceptTcpClient();

                con = new Connection();
                con.stream = client.GetStream();
                con.streamr = new StreamReader(con.stream);
                con.streamw = new StreamWriter(con.stream);

                con.nick = con.streamr.ReadLine();

                list.Add(con);
                Console.WriteLine(con.nick + "is conected.");

                Thread t = new Thread(Escuchar_conexion);

                t.Start();
            }
        }

        void Escuchar_conexion()
        {
            Connection hcon = con;

            do
            {
                try
                {
                    string tmp = hcon.streamr.ReadLine();
                    Console.WriteLine(hcon.nick + ": " + tmp);
                    foreach (Connection c in list)
                    {
                        try
                        {
                            c.streamw.WriteLine(hcon.nick + ": " + tmp);
                            c.streamw.Flush();
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                    list.Remove(hcon);
                    Console.WriteLine(con.nick + " se a desconectado.");
                    break;
                }
            } while (true);
        }

    }
}
