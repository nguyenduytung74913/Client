﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace clientt
{
    class Program
    {
        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 9999;

        static ASCIIEncoding encoding = new ASCIIEncoding();

        public static void Main()
        {

            try
            {
                TcpClient client = new TcpClient();

                // 1. connect
                client.Connect("127.0.0.1", PORT_NUMBER);
                Stream stream = client.GetStream();

                Console.WriteLine("Connected to Server.");

                while (true)
                {


                    var reader = new StreamReader(stream);
                    var writer = new StreamWriter(stream);
                    writer.AutoFlush = true;

                    Console.WriteLine("Please enter the number (0-10) : ");

                    // 2. send
                    string str = Console.ReadLine();
                    writer.WriteLine(str);
                    writer.AutoFlush = true;

                    // 3. receive
                    str = reader.ReadLine();
                    Console.WriteLine(str);
                    if (str.ToUpper() == "BYE")
                        break;
                }
                // 4. close
                stream.Close();
                client.Close();
            }

            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(";Disconnect At: " + DateTime.Now + ";" + "Reason: Closed By Server\n");
                File.AppendAllText("D://Access.log", sb.ToString());
                sb.Clear();
                Console.WriteLine("Error: " + ex);
            }

            Console.Read();
        }
    }
}
