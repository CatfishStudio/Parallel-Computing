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
				int indexI = 9;
				int indexJ = 9;
				int[,] array2D = new int[indexI,indexJ];
				int result = 0;
				
                Intracommunicator comm = Communicator.world;
                
            	if (comm.Rank == 0)
            	{
            		HeaderShow();
            		ArrayFill(ref array2D, indexI, indexJ);
				
            		int IndexRank = 1;
            		for (int i = 0; i < indexI; i++){
						for (int j = 0; j < indexJ; j++){
            				int[] arrayObject = new int[2] {i, j};
            				// Передаём процессу
            				comm.Send(arrayObject, IndexRank, 0);
            				
						}
            			IndexRank++;
					}
            		
            		//ResultShow(result);
            		
            		comm.Barrier(); // барьер.
            		
            	}
            	else // not rank 0
            	{
            		
            		int[] arrayObject = new int[2];
            		// Процесс принимает
            		comm.Receive(0, 0, ref arrayObject);
            		for(int i = 0; i < arrayObject.Length; i++)
					{
						Console.Write("{0}\t", arrayObject[i]);
					}
            		
            		//result = ResultProcessingElements(GetLine(arrayObject[0], array2D));
            		//comm.Gather(); // записываем результат
            		comm.Barrier(); // барьер.
            		
            		
            		
            		
            		
            	}
            	
            	
            	
			}
			Console.Read();
		}
		
		public static void HeaderShow()
		{
			Console.WriteLine();
			Console.Write("ПРОГРАММА: Параллельные вычисления.");
			Console.WriteLine();
			Console.Write("АВТОР: студент ВНУ, гр. МТз-401, Сомов Е.П.");
			Console.WriteLine();
			Console.WriteLine();
		}
		
		public static void ArrayFill(ref int[,] _array2D, int _i, int _j)
		{
			Console.Write("Инициализация массива слечайных чисел:");
			Console.WriteLine();
			Console.Write("---------------------------------------------------------------------------");
			Console.WriteLine();
			
			Random ran = new Random();
			for (int i = 0; i < _i; i++){
				for (int j = 0; j < _j; j++){
					_array2D[i, j] = ran.Next(1, 15);
					Console.Write("{0}\t", _array2D[i, j]);
				}
			}
			Console.WriteLine();
			Console.Write("--------------------------------------------------------------------------");
			Console.WriteLine();
		}
			
		
		public static void ResultShow(int _result)
		{
			Console.WriteLine();
			Console.Write("ЗАДАЧА: Посчитать количество элементов встречающихся в массиве больше двух раз.");
			Console.WriteLine();
			Console.Write("РЕЗУЛЬТАТ: " + _result.ToString());
			Console.WriteLine();
			Console.WriteLine();
		}
		
		/* Получить столбец */
		public static int[] GetColumn(int idColumn, int[,] M)
		{
			int[] _result = new int[M.GetLength(0)];
			for(int i = 0; i < _result.Length; i++)
			{
				_result[i] = M[i, idColumn];
			}
			return _result;
		}
		
		/* Получить строку */
		public static int[] GetLine(int idLine, int[,] M)
		{
			int[] _result = new int[M.GetLength(0)];
			for(int i = 0; i < _result.Length; i++)
			{
				_result[i] = M[idLine, i];
			}
			return _result;
		}
		
		/* Результат обработки */
		public static int ResultProcessingElements(int[] _line)
		{
			for(int i = 0; i < _line.Length; i++)
			{
				Console.Write("{0}\t", _line[i]);
			}
			return 0;
		}
	}
}