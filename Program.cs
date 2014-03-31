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
				Console.WriteLine("Hello, World! from rank " + Communicator.world.Rank
                  + " (running on " + MPI.Environment.ProcessorName + ")");
			}
			
		}
	}
}