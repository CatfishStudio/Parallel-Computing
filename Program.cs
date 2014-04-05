/*
 * Сделано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 31.03.2014
 * Время: 11:21
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Linq;
using MPI;

namespace ParallelComputing
{
	class Program
	{
		public static int[,] Matrix = new int[9,9]{
			{10,2,6,10,5,8,10,1,9},
			{1,20,3,4,5,20,7,8,20},
			{2,2,3,4,5,6,7,8,9},
			{40,30,30,30,5,40,40,8,9},
			{50,2,3,50,5,50,7,50,9},
			{60,2,60,4,5,70,70,8,9},
			{80,80,3,90,80,6,90,8,90},
			{95,2,95,4,5,95,7,8,9},
			{10,10,1,10,10,1,10,1,10}
		};
		public static int[] Result = new int[9];
			
		public static void Main(string[] args)
		{
			// вызывается строкой: C:\GIT\ParallelComputing\bin\Debug>"C:\Program Files\Microsoft Compute Cluster Pack\Bin\mpiexec.exe" -n 8 ParallelComputing.exe
			
			using (new MPI.Environment(ref args))
			{               
				int indexI = 9; // строка
				int indexJ = 9; // столбец
				
                Intracommunicator comm = Communicator.world;
                
            	if (comm.Rank == 0)
            	{
            		HeaderShow(); // отображаем шапку
            		ShowTable(Matrix, indexI, indexJ);
            	
            		int IndexRank = 1;
            		for (int i = 0; i < indexI; i++){
						for (int j = 0; j < indexJ; j++){
            				int[] coordinate = new int[2] {i, j}; // координата
            				/* Передаём процессу координату начала массива */
            				comm.Send(coordinate, IndexRank, 0);
            				
						}
            			IndexRank++;
					}
            		
            		comm.Gather(-1, 0, ref Result); // записываем результат
            		comm.Barrier(); // барьер.
            		ResultShow(Result); // Показать результат
            		
            		
            	}
            	else // not rank 0
            	{
            		
            		
            		int[] coordinate = new int[2];
            		int _result = 0;
            		// Процесс принимает
            		comm.Receive(0, 0, ref coordinate);
            		for(int i = 0; i < (coordinate.Length / 2); i++)
					{
            			_result = ResultProcessing(GetLine(coordinate[i], Matrix));
            			
            		}
            		
            		comm.Gather(_result, 0, ref Result); // записываем результат
            		comm.Barrier(); // барьер.
            	}
           	}
			Console.Read();
		}
		
		/* Показать шапку програмы*/
		public static void HeaderShow()
		{
			Console.WriteLine();
			Console.Write("ПРОГРАММА: Параллельные вычисления.");
			Console.WriteLine();
			Console.Write("АВТОР: студент ВНУ, гр. МТз-401, Сомов Е.П.");
			Console.WriteLine();
			Console.WriteLine();
		}
		
		/* Показат иследуемый массив */
		public static void ShowTable(int[,] _matrix, int _i, int _j)
		{
			Console.Write("Инициализация массива слечайных чисел:");
			Console.WriteLine();
			Console.Write("---------------------------------------------------------------------------");
			Console.WriteLine();
			for (int i = 0; i < _i; i++){
				Console.Write("Строка:[" + i.ToString() + "] ");
				for (int j = 0; j < _j; j++){
					Console.Write("{0}\t", _matrix[i, j]);
				}
				Console.WriteLine();
			}			
			Console.WriteLine();
			Console.Write("--------------------------------------------------------------------------");
			Console.WriteLine();
			
		}
			
		/* Показать результат */
		public static void ResultShow(int[] _result)
		{
			Console.WriteLine();
			Console.Write("ЗАДАЧА: Посчитать количество элементов встречающихся в массиве больше двух раз.");
			Console.WriteLine();
			Console.Write("РЕЗУЛЬТАТ: ");
			Console.WriteLine();
			for(int i = 1; i < _result.Length; i++)
			{
				Console.WriteLine("Строка [" + i.ToString() + "] повторов: " + _result[i].ToString());
			}
			Console.WriteLine();
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
		public static int ResultProcessing(int[] _line)
		{
			int _result = 0;
            Array.Sort(_line); // сортировать.
            
            var g = _line.GroupBy(i => i);
            foreach(var k in g){
            	if(k.Count() > 2 ) _result++;
            }
            return _result;
		}
	}
}