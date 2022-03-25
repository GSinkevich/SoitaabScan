using SoitaabScan.Model;
using System;
using System.Threading;

namespace SoitaabScan.Model
{
    class Program
    {
        static void Main(string[] args)
        {

            string IgnorFolder = "Выполнено";
            string folder1 = @"D:\TEST";
          
            ConsoleSettings();

            ClientSoitaabScan clientSoitaabScan = new ClientSoitaabScan(ref folder1, ref IgnorFolder);
            clientSoitaabScan.ProgrammStarted += ClientSoitaabScan_Started;

            string result;

            clientSoitaabScan.ClientStart();

            while (true)
            {
                result = String.Empty;

                
                Console.Clear();
                result = clientSoitaabScan.GetListPrograms();
                Console.WriteLine(result);

                Console.Write("Показать только лист : ");
                string input_value = Console.ReadLine();

                if (int.TryParse(input_value, out int j))
                {
                    Console.Clear();
                    Console.WriteLine($"{"Название",23} {"Толщина",10} {"Габариты ",14} {"Остаток",20} {"Дата создания",20}");
                    Console.WriteLine(clientSoitaabScan.GetProgramsOnlyOneThickness(j));
                    Console.ReadKey();
                }

                if (input_value.ToLower() == "d")
                {
                    Console.Clear();
                    Console.WriteLine("Сортировка по дате");
                    Console.WriteLine($"{"Название",23} {"Толщина",10} {"Габариты ",14} {"Остаток",20} {"Дата создания",20}");
                    Console.WriteLine(clientSoitaabScan.SortByDate());
                    Console.ReadKey();
                }

               

                Console.Clear();
            }

           
        }
       



        private static void ClientSoitaabScan_Started(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(1000);
            Console.Clear();
        }

        private static void ConsoleSettings()
        {
            Console.SetWindowSize(160, 53);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }

    
}