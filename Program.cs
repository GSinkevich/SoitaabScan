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
            //string folder = @"D:\Test";
            string IgnorFolder = "Выполнено";

            string folder = @"X:\SOITAAB";
           
            //int x = 150;
            //int y = 53;
            //var key = Console.ReadKey().Key;
            //if (key == ConsoleKey.F3)
            //{
            //    Console.WriteLine("Введите новый X");
            //    x = Convert.ToInt32(Console.ReadLine());
            //    Console.WriteLine($"{x} : новый");
            //    Console.ReadLine();

            //}
            //if (key == ConsoleKey.F4)
            //{
            //    Console.WriteLine("Введите новый Y");
            //    y = Convert.ToInt32(Console.ReadLine());
            //    Console.WriteLine($"{y} : новый");

            //}
            Console.SetWindowSize(160, 53);
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
                while (true)
                {

                    DirectoryInfo d = new DirectoryInfo(folder);

                    ProgramList programList = new ProgramList(d, IgnorFolder);


                    Console.Clear();
                    Console.WriteLine($"Список программ {folder},кроме {IgnorFolder}");


                    Console.WriteLine($"{"Название",23} {"Толщина",10} {"Габариты ",14} {"Остаток",20} {"Дата создания",20}");

                    programList.DefaultSort();

                    Console.WriteLine(programList.GetStringInfoAllPrograms());

                    Console.Write("Показать только лист : ");
                    string input_value = Console.ReadLine();

                    if (int.TryParse(input_value, out int j))
                    {
                        Console.Clear();
                        Console.WriteLine($"{"Название",23} {"Толщина",10} {"Габариты ",14} {"Остаток",20} {"Дата создания",20}");
                        Console.WriteLine(programList.GetProgramsOnlyOneThickness(j));
                        Console.ReadKey();
                    }
                    if (input_value.ToLower() == "d")
                    {
                        Console.Clear();
                        Console.WriteLine("Сортировка по дате");
                        Console.WriteLine($"{"Название",23} {"Толщина",10} {"Габариты ",14} {"Остаток",20} {"Дата создания",20}");
                        Console.WriteLine(programList.SortByDate());
                        Console.ReadKey();
                    }
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }
       
    }
}
