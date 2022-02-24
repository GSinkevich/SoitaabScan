using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SoitaabScan
{
    class Program
    {
        static void Main(string[] args)
        {
           string folder = @"D:\Test";
           //string folder = @"X:\SOITAAB";
            string ignorpath = "Выполнено";

            //int x = 150;
            //int y = 53;
            //var key = Console.ReadKey().Key;
           // var key = ConsoleKey.Enter;
            //if (key == ConsoleKey.F3)
            //{
            //    Console.WriteLine("Введите новый X");
            //    x = Convert.ToInt32(Console.ReadLine());
            //    Console.WriteLine($"{x} : новый");
            //    Console.ReadLine();
            //    Console.ReadLine();

            //}
            //if (key == ConsoleKey.F4)
            //{
            //    Console.WriteLine("Введите новый Y");
            //    y = Convert.ToInt32(Console.ReadLine());
            //    Console.WriteLine($"{y} : новый");

            //}
            //Console.SetWindowSize(x, y);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;


            //if (key == ConsoleKey.F1)
            //{
            //    Console.WriteLine("Введите новый путь");
            //    folder = Console.ReadLine();
            //    Console.WriteLine($"{folder} : новый путь");

            //}
            //if (key == ConsoleKey.F2)
            //{
            //    Console.WriteLine("Введите новый путь игнора");
            //    folder = Console.ReadLine();
            //    Console.WriteLine($"{ignorpath} : новый путь");

            //}
            try
            {
                DirectoryInfo d = new DirectoryInfo(folder);

                ProgramList programList = new ProgramList(d);

                bool flag = true;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Список программ {folder},кроме {ignorpath}");


                    Console.WriteLine($"{"Название",23} {"Толщина",10} {"Габариты ",14} {"Остаток",20} {"Дата создания",20}");
                    
                    programList.DefaultSort();

                    Console.WriteLine(programList.GetStringInfoAllPrograms());

                    Console.Write("Показать только лист : ");
                    string enter = Console.ReadLine();

                    if (int.TryParse(enter, out int j))
                    {
                        Console.Clear();
                        Console.WriteLine($"{"Название",23} {"Толщина",10} {"Габариты ",14} {"Остаток",20} {"Дата создания",20}");
                        Console.WriteLine(programList.GetProgramsOnlyOneThickness(j));
                        Console.ReadKey();
                        Console.WriteLine("Нажмите любую кнопку для продолжения");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Введите корректную толщину листа");
                        Thread.Sleep(1550);
                    }



                } while (flag);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }



        }
       
    }
}
