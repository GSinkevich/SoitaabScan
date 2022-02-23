using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoitaabScan
{
    class ProgramList
    {
        public ProgramSoitaab[] programSoitaabs = new ProgramSoitaab[500];

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

        public void GetInfoAllPrograms()
        {
            for (int i = 0; i < programs.Length ; i++)
            {
                programSoitaabs[i] = new ProgramSoitaab(programs[i]);
            }
        }

        public override string ToString()
        {
            string res = string.Empty;
            foreach (var program in programSoitaabs)
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

        
    }
}
 