using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleThreadSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов массива: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество потоков: ");
            int threadsAmount = Convert.ToInt32(Console.ReadLine());
            Random rand = new Random();
            int[] array = new int[quantity];
            int[] array2 = new int[quantity];
            int[] array3 = new int[quantity];
            for (int i = 0; i < quantity; i++)
            {
                array[i] = rand.Next(100);
                array2[i] = array[i];
                if (array.Length <= 100)
                {
                    Console.Write(array[i] + " ");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Отсортировать");
            Console.ReadLine();
            Console.WriteLine("Параллельно");
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff"));
            Sort.ThreadBubbleSort2(ref array, threadsAmount);
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff"));
            if (array.Length <= 100)
            {
                for (int i = 0; i < quantity; i++)
                {
                    Console.Write(array[i] + " ");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Обычная");
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff"));
            Sort.Bubble_Sort(array2);
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff"));
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
