/*
 * Сделано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 31.03.2014
 * Время: 11:21
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using MPI;

namespace ParallelComputing
{
	class Program
	{
		public static void Main(string[] args)
		{
			// вызывается строкой: C:\GIT\ParallelComputing\bin\Debug>"C:\Program Files\Microsoft Compute Cluster Pack\Bin\mpiexec.exe" -n 8 ParallelComputing.exe
			
			using (new MPI.Environment(ref args))
			{               
               Intracommunicator comm = Communicator.world;
            	if (comm.Rank == 0)
            	{
                	// program for rank 0
    				comm.Send("Rosie", 1, 0);

    				// receive the final message
    				string msg = comm.Receive<string>(Communicator.anySource, 0);

    				Console.WriteLine("Rank " + comm.Rank + " received message \"" + msg + "\".");
            	}
            	else // not rank 0
            	{
                	// program for all other ranks
   					string msg = comm.Receive<string>(comm.Rank - 1, 0);

    				Console.WriteLine("Rank " + comm.Rank + " received message \"" + msg + "\".");

    				comm.Send(msg + ", " + comm.Rank, (comm.Rank + 1) % comm.Size, 0);
                	
            	}
            	
            	
            	/*
				Console.WriteLine("Hello, World! from rank " + Communicator.world.Rank
                  + " (running on " + MPI.Environment.ProcessorName + ")");
                */
			}
			
		}
	}
}