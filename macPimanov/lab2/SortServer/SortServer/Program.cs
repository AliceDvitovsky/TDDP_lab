﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SortLibrary;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace SortServer
{
    class Server
    {
        TcpChannel channel;
        public void Start()
        {
            channel = new TcpChannel(8081);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SharedObject), "Пул данных", WellKnownObjectMode.Singleton);
            Log.Print("Сервер запущен");
        }

        public void Stop()
        {
            ChannelServices.UnregisterChannel(channel);
            Log.Print("Сервер остановлен");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Server srv = new Server();
            srv.Start();
            Console.In.ReadLine();
            srv.Stop();
        }
    }
}
