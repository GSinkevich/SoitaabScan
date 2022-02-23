using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoitaabScan
{
    class ProgramSoitaab : IComparable<ProgramSoitaab>
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

        public ProgramSoitaab(FileInfo file)
        {
            _file = file;
            Name = file.Name;
            GetSizeXY();
            DateTime = file.CreationTime.Date;
        }

   
        public int CompareTo(ProgramSoitaab obj)
        {
            if (this.Thickness > obj.Thickness)
                return 1;
            if (this.Thickness < obj.Thickness)
                return -1;
            else
                return 0;
        }



        public void GetSizeXY()
        {
            string line;
            string alltext;
            string[] allcicl;
            using (StreamReader sr = new StreamReader(_file.FullName))
            {

                if ((line = sr.ReadLine()) != null)
                {
                    line = sr.ReadLine();
                }

                alltext = sr.ReadToEnd();
            }

            string[] secondline = line.Split(' ');
            SizeX = Convert.ToInt32(secondline[6]);
            SizeY = Convert.ToInt32(secondline[4]);
            Thickness = Convert.ToInt32(secondline[8]);

            if (_file.FullName.Contains("P"))
            {
                Ostatok = "Пистолет";
                return;
            }

            allcicl = alltext.Split(']');
            line = allcicl[allcicl.Length - 1];

            allcicl = line.Split(' ', '.');

            List<int> listX = new List<int>();
            List<int> listY = new List<int>();

            foreach (var item in allcicl)
            {
                if (item.Contains("X"))
                {
                    var value = Convert.ToInt32(item.Remove(0, 1));
                    listX.Add(value);

                }
                if (item.Contains("Y"))
                {
                    var value = Convert.ToInt32(item.Remove(0, 1));
                    listY.Add(value);

                }
            }

            Xmax = listX.Max();
            Xmin = listX.Min();

            Ymax = listY.Max();
            Ymin = listY.Min();

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
