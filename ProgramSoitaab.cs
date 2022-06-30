using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoitaabScan
{
    class ProgramSoitaab 
    {

        static private FileInfo _file;
        public string Name { get; set; }
        public string Ostatok { get; set; }
        public int Thickness { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        private int Xmax, Ymax;
        private int Xmin ,Ymin;

        public DateTime DateTime { get; }

        public ProgramSoitaab GetProgram()
        {
            return new ProgramSoitaab(_file);
        }

        public ProgramSoitaab(FileInfo file)
        {
            _file = file;
            Name = file.Name;
            DateTime = file.LastWriteTime.Date;
            GetSizeXY();
        }

        public void GetSizeXY()
        {
            string[] secondline;
            string[] AllstringText;
            AllstringText = File.ReadAllLines(_file.FullName);


            secondline = AllstringText[1].Split(' ');

            var a =  secondline[6].Split('.');

            SizeX = Convert.ToInt32(a[0]);


            var b = secondline[4].Split('.');

            SizeY = Convert.ToInt32(b[0]);


            int.TryParse(secondline[8],out int asd);

            Thickness = asd;

            if (_file.FullName.Contains("Q"))
            {
                Ostatok = "Пистолет";
                return;
            }

            int indexlastblockstarted = GetIndexLastblock(AllstringText);



            List<int> listX = new List<int>();
            List<int> listY = new List<int>();

            for (int i = indexlastblockstarted; i < AllstringText.Length-1 ; i++)
            {
                if (AllstringText[i].Contains("X") || AllstringText[i].Contains("Y"))
                {
                    string[] bufer = AllstringText[i].Split(',', '.', ' ') ;

                    foreach (var item in bufer)
                    {
                        if (item.Length == 0)
                        {
                            break;
                        }

                        if (item[0] == 'X')
                        {
                            listX.Add(Convert.ToInt32(item.Remove(0, 1)));
                        }

                        if (item[0] == 'Y')
                        {
                            listY.Add(Convert.ToInt32(item.Remove(0, 1)));
                        }
                    }
                }
            }

            if (listX.Count != 0 || listY.Count !=0 )
            {
                Xmax = listX.Max();
                Xmin = listX.Min();
                Ymax = listY.Max();
                Ymin = listY.Min();
            }
            

            int X, Y;

            if (CheckMaxMin())
            {
                X = Xmax - Xmin;
                Y = Ymax - Ymin;
                if (X == 0)
                {
                    X = SizeX - Xmax;
                }
                if (Y == 0 )
                {
                    Y = SizeY - Ymax;
                }

                Ostatok = $"{Y} x {X}";
            }
            else
            {
                Ostatok = "   ---";
            }

        }

        private int GetIndexLastblock(string [] allstring)
        {
            int Indexlastblock = 0;
            for (int i = 2; i < allstring.Length; i++)
            {
                if (allstring[i].Contains(']'))
                {
                    Indexlastblock = i;
                }

                continue;
            }
            return Indexlastblock;
        }

        private bool CheckMaxMin()
        {
            if (Xmax == 0 | Xmin == 0| Ymax == 0 | Ymin == 0)
            {
                return true;
            }
             
            if (Xmax == SizeX | Xmin == SizeX | Ymax == SizeY | Ymin == SizeY)
            {
                return true;
            }

            return false;
        }

      
    }
}
