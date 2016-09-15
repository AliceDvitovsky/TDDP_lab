﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixLibrary;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace MatrixClient
{
    class Shell
    {
        TcpChannel chan;
        SharedObject obj;

        Task task;

        public Shell()
        {
            chan = new TcpChannel();
            ChannelServices.RegisterChannel(chan, false);
            obj = (SharedObject)Activator.GetObject(typeof(MatrixLibrary.SharedObject), "tcp://localhost:8081/DataPool");
        }

        public int sort()
        {
            
        
                task = obj.GetTask();
      
                if (task == null)
                    return 0;
                double[,] arr;
                double[] b1;
                double[] C;
                 obj.FetchData(task, out b1, out arr);
               C= new double[SharedObject.n];
                Console.Out.Write("Полученные данные:");
                    
                for (int i = task.start; i < task.stop; i++)
                {
                    for (int j = 0; j < SharedObject.n; j++)
                    {
                        Console.Out.Write(arr[i,j]+" ");

                    }
                    Console.Out.WriteLine();
                }

           for (int i = task.start; i < task.stop; i++)
            {
                for (int j = 0; j < SharedObject.n; j++)
                {
                    C[i] += arr[i, j] * b1[j];

                }
            }


                obj.Finish(C);

            return 1;
        }

    }

    class Program
    {


        static void Main(string[] args)
        {
            Shell shellObj = new Shell();
            Console.Out.WriteLine("Клиент запущен");

            while (shellObj.sort() != 0)
                Console.In.ReadLine();

            Console.Out.WriteLine("Задачи кончились, нажмите Enter...");
            Console.ReadLine();
            

        }
    }
}
