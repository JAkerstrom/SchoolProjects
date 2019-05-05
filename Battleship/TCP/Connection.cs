using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace johanna_battleship.TCP
{
    public class Connection
    {
        private int listenOnPort;
        private TcpListener listener = null;
        private TcpClient client = null;
        private NetworkStream networkStream = null;
        private StreamWriter writer = null;
        private StreamReader reader = null;
        private string myName = "Johanna";
        public bool Connected = false;

        public Connection(int port)
        {
            listenOnPort = port;
        }

        public void startListening()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, listenOnPort);
                listener.Start();
                client = listener.AcceptTcpClient();
                networkStream = client.GetStream();
                writer = new StreamWriter(networkStream) { AutoFlush = true };
                reader = new StreamReader(networkStream);
                Console.WriteLine($"Ansluten till {client.Client.RemoteEndPoint}");
                if (client.Connected) Connected = true;

            }
            catch (Exception exception)
            {
                CloseAll();
                Console.WriteLine($"Exception when establishing connection:::: {exception}");
            }


        }

        public void CloseAll()
        {
            reader.Close();
            writer.Close();
            networkStream.Close();
            listener.Stop();
            Console.WriteLine("Server says: The client is disconnected");
        }

        public bool WriteLine(string command)
        {          
            try
            {
                writer.WriteLine(command);
                Console.WriteLine("Server says: " + command);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Server write error:::: " + e.Message);
                CloseAll();
                return false;
            }      
        }

        public string ReadLine()
        {
            string response = " ";

            try
            {
                response = reader.ReadLine();
                if (response == null) Console.WriteLine("Client says: null");
                else Console.WriteLine($"Client says: {response}");
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Server read error:::: " + e.Message);
                CloseAll();
                return response;               
            }
        }
    }
}
