using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoitaabScan
{
     class ProgramList
    {
        private List<ProgramSoitaab> AllrpogramsSoitaab = new List<ProgramSoitaab>();

        public List<ProgramSoitaab> selected_programs = new List<ProgramSoitaab>();

        private FileInfo[] programs;
        
        public ProgramList(DirectoryInfo d)
        {
            if (d is null)
            {
                throw new ArgumentNullException(nameof(d), "is null");
            }

            programs = GetOnlypPogram(d);
            GetInfoAllPrograms();
        }

        private void GetInfoAllPrograms()
        {
            foreach (var item in programs)
            {
                AllrpogramsSoitaab.Add(new ProgramSoitaab(item));
            }
        }

        public string GetProgramsOnlyOneThickness(int thickness)
        {
            string res = string.Empty;
            foreach (var program in AllrpogramsSoitaab)
            {
                if (program.Thickness == thickness)
                {
                    res += $"\t{program.Name,-15} \t{program.Thickness,1} \t{program.SizeY} x {program.SizeX}  \t{program.Ostatok,-15} \t{program.DateTime.ToString("dd/MM"),3} \n";
                }
            }
            if (res == string.Empty)
            {
                return "нету таких программ";
            }
            return res;
        }

        public string GetStringInfoAllPrograms() 
        {
            string res = string.Empty;
            foreach (var program in AllrpogramsSoitaab)
            {
                if (program is null)
                {
                    return res;
                }

                res += $"\t{program.Name,-15} \t{program.Thickness,1} \t{program.SizeY} x {program.SizeX}  \t{program.Ostatok,-15} \t{program.DateTime.ToString("dd/MM"),3} \n";
                
            }
            return res;
        }

        private FileInfo[] GetOnlypPogram(DirectoryInfo directoryInfo)
        {
            List<FileInfo> res = new List<FileInfo>();

            var AllFiles = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

            if (AllFiles.Length == 0)
            {
                throw new ArgumentException(nameof(directoryInfo), "is empty");
            }



            foreach (FileInfo currentFile in AllFiles)
            {
                var Extension = Path.GetExtension(currentFile.ToString());

                if (Extension == "")
                {
                    string path = currentFile.FullName.ToString();
                    string secondLine = File.ReadLines(path).Skip(1).First();



                    if (/*!currentFile.FullName.Contains(ignorpath) &&*/secondLine.Contains("PLASMA PREMERE START"))
                    {
                        res.Add(currentFile);
                    }
                }

            }
            return res.ToArray();

        }

       public void DefaultSort()
        {
            if (AllrpogramsSoitaab is null)
            {
                throw new ArgumentNullException(nameof(AllrpogramsSoitaab));
            }

            byte swap;

            do
            {
                swap = 0;

                for (int i = 1; i < AllrpogramsSoitaab.Count; i++)
                {
                    if (AllrpogramsSoitaab[i] is null)
                    {
                        continue;                        
                    }

                    if (AllrpogramsSoitaab[i].Thickness < AllrpogramsSoitaab[i - 1].Thickness)
                    {
                        swap += 1;
                        var variabletoexchange = AllrpogramsSoitaab[i];
                        AllrpogramsSoitaab[i] = AllrpogramsSoitaab[i - 1];
                        AllrpogramsSoitaab[i - 1] = variabletoexchange;
                    }
                }
            }
            while (swap > 0);
        }
    }
}
 