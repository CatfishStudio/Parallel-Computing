/*
 * Сделано в SharpDevelop.
 * Пользователь: Somov Evgeniy
 * Дата: 06.04.2014
 * Время: 8:48
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Linq;

namespace SuccessiveComputing
{
	class Program
	{
		public static int[,] Matrix = new int[9,9]{
			{10,2,6,10,5,8,10,1,9},
			{1,20,3,4,5,20,7,8,20},
			{2,2,3,4,5,6,7,8,9},
			{40,30,30,30,5,40,40,8,9},
			{50,2,3,50,5,50,7,7,9},
			{60,2,60,4,5,70,70,8,9},
			{80,80,3,90,80,6,90,8,90},
			{95,2,95,4,5,95,7,8,9},
			{10,10,1,10,10,1,10,1,10}
		};
		public static int[] Result = new int[9];
		
		public static void Main(string[] args)
		{
			HeaderShow(); // отображаем шапку
            ShowTable(Matrix, 9, 9);
            		
			for (int i = 0; i < 9; i++){
            	
            	Result[i] = ResultProcessing(GetLine(i, Matrix));
          	}
			
            ResultShow(Result);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		/* Показать шапку програмы*/
		public static void HeaderShow()
		{
			Console.WriteLine();
			Console.Write("ПРОГРАММА: Последовательныt вычисления.");
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
				Console.WriteLine("Строка [" + i.ToString() + "] найдено повторов: " + _result[i].ToString());
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