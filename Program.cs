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
			{1,2,3,4,5,6,7,8,9},
			{2,2,3,4,5,6,7,8,9},
			{3,2,3,4,5,6,7,8,9},
			{4,2,3,4,5,6,7,8,9},
			{5,2,3,4,5,6,7,8,9},
			{6,2,3,4,5,6,7,8,9},
			{7,2,3,4,5,6,7,8,9},
			{8,2,3,4,5,6,7,8,9}
		};
			
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
            		
            		//ResultShow(result);
            		
            		comm.Barrier(); // барьер.
            		
            	}
            	else // not rank 0
            	{
            		
            		
            		int[] coordinate = new int[2];
            		// Процесс принимает
            		comm.Receive(0, 0, ref coordinate);
            		for(int i = 0; i < (coordinate.Length / 2); i++)
					{
            			String coord = "Строка: " + coordinate[i].ToString() + " ; Столбец:" + coordinate[i+1];
						Console.Write(coord);
						Console.Write("   повторений: ");
						Console.Write(ResultProcessing(GetLine(coordinate[i], Matrix)).ToString());
						Console.WriteLine();
						Console.WriteLine();
						Console.Write("---------------------------------------------------------------------------");
						Console.WriteLine();
						Console.WriteLine();
					}
            		
            		//result = ResultProcessingElements(GetLine(arrayObject[0], array2D));
            		//comm.Gather(); // записываем результат
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
		
		/* Инициализация исходного массива */
		public static int[,] ArrayFill(int _i, int _j)
		{
			
			Random ran = new Random();
			int[,] _matrix = new int[_i,_j];
			for (int i = 0; i < _i; i++){
				Console.Write("Строка:[" + i.ToString() + "] ");
				for (int j = 0; j < _j; j++){
					_matrix[i, j] = ran.Next(1, 15);
					Console.Write("{0}\t", _matrix[i, j]);
				}
				Console.WriteLine();
			}
			return _matrix;
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
		public static int ResultProcessing(int[] _line)
		{
			//List<int> ages = new List<int> { 21, 46, 46, 55, 17, 21, 55, 55 };
            //IEnumerable<int> distinctAges = ages.Distinct();
            //IEquatable<int> _result = _line.Distinct().Count();
            //return (int)_result;
            
            Array.Sort(_line); // сортировать.
            for(int i = 0; i < _line.Length; i++)
			{
            	Console.Write(_line[i].ToString());
            }
            Console.Write(" | ");
            var g = _line.GroupBy(i => i);
           
            return g.Count();
		}
	}
}