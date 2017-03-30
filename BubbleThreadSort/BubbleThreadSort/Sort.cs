using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace BubbleThreadSort
{
    public static class Sort
    {
        public static void Bubble_Sort(int[] array2)
        {
            for (int k = array2.Length - 1; k > 0; k--)
                for (int i = 0; i < k; i++)
                    if (array2[i] > array2[i + 1])
                    {
                        Swap(ref array2[i], ref array2[i + 1]);
                    }

        }

        public static void Swap(ref int aFirstArg, ref int aSecondArg)
        {
            int tmpParam = aFirstArg;
            aFirstArg = aSecondArg;
            aSecondArg = tmpParam;
        }

        public static void ThreadBubbleSort(int[] array)
        {
            int n = array.Length;

            Thread[] thr1 = new Thread[(int)n / 2];
            Thread[] thr2 = new Thread[(int)n / 2];
            int count1;
            int count2;
            for (int i = 0; i < n - 1; i++)
            {
                if (i % 2 == 0)//если i четное
                {
                    count1 = 0;
                    for (int j = 0; j < n / 2; j++)
                    {
                        int temp = j;
                        count1++;
                        thr1[temp] = new Thread(() =>
                          {
                              if (array[2 * temp] > array[2 * temp + 1])
                              {
                                  Swap(ref array[2 * temp], ref array[2 * temp + 1]);
                              }
                          });
                        thr1[temp].Start();
                    }
                    for (int m = 0; m < count1; m++)
                    {
                        thr1[m].Join();
                    }
                }
                else//если i нечётное
                {
                    count2 = 0;
                    for (int k = 0; k < (n % 2 == 0 ? n / 2 - 1 : n / 2); k++)
                    {
                        int temp = k;
                        count2++;
                        thr2[temp] = new Thread(() =>
                          {
                              if (array[2 * temp + 1] > array[2 * temp + 2])
                              {
                                  Swap(ref array[2 * temp + 1], ref array[2 * temp + 2]);
                              }
                          });
                        thr2[temp].Start();
                    }
                    for (int m = 0; m < count2 - 1; m++)
                    {
                        thr2[m].Join();
                    }
                }

            }


        }

       
        private static int[] Merge(int[] left, int[] right)
        {
            int[] buff = new int[left.Length + right.Length];
            int i = 0;  //объединенный массив
            int l = 0;  //левый массив
            int r = 0;  //правый массив
            for (; i < buff.Length; i++)
            {
                if (r >= right.Length)
                {
                    buff[i] = left[l];
                    l++;
                }
                else if (l < left.Length && left[l] < right[r])
                {
                    buff[i] = left[l];
                    l++;
                }
                else
                {
                    buff[i] = right[r];
                    r++;
                }
            }
            return  buff;
        }

        public static void ThreadBubbleSort2(ref int[] array, int threadsAmount)
        {
            List<Thread> threadList = new List<Thread>();
            List<int[]> secondaryArrayList = new List<int[]>();
            int n = array.Length;
            int secondaryArrayCount = n / threadsAmount;

            for (int threadId = 0; threadId < threadsAmount; threadId++)// проходим все потоки в цикле
            {
                Thread thread = new Thread(o => // кажый поток сортирует пузырьком свою часть массива
                {
                    int[] subArray = (int[])o;
                    for (int x = subArray.Length - 1; x > 0; x--)
                        for (int i = 0; i < x; i++)
                            if (subArray[i] > subArray[i + 1])
                            {
                                Swap(ref subArray[i], ref subArray[i + 1]);
                            }
                });

                int[] subArray2 = new int[0];
                int k = 0;
                for (int i = threadId * secondaryArrayCount; threadId < (threadsAmount - 1) ? i < (threadId + 1) * secondaryArrayCount : i < n; i++)// копирует элементы массива в подмассив
                {
                    Array.Resize<int>(ref subArray2, subArray2.Length + 1);
                    subArray2[k] = array[i];
                    k++;
                }
                secondaryArrayList.Add(subArray2);
                threadList.Add(thread);
                thread.Start(subArray2);
            }

            foreach (Thread thread in threadList)// ожидаем завершения работы потоков
            { 
                thread.Join();
            }

            array = secondaryArrayList[0];
            for (int i = 1; i < secondaryArrayList.Count; i++)// объединяем подмассивы в один
            {
                array = Merge(array, secondaryArrayList[i]);
            }
        }
    }
}







