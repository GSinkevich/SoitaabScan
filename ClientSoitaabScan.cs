using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoitaabScan
{
    class ClientSoitaabScan
    {
        ProgramList programList;

        public delegate void AccountHandler(string message);

        public event AccountHandler ProgrammStarted;

        public ClientSoitaabScan(ref string path, ref string IgnorFolder)
        {
            programList = new ProgramList(new DirectoryInfo(path), ref IgnorFolder);
        }

        public void ClientStart()
        {
            programList.GetInfoAllPrograms();
            ProgrammStarted.Invoke("Program started , waite please");
        }

        public string GetListPrograms()
        {
            return programList.GetStringInfoAllPrograms();
        }

        public string GetProgramsOnlyOneThickness(int thicknes)
        {
            return programList.GetProgramsOnlyOneThickness(thicknes);
        }

        public string SortByDate()
        {
            return programList.SortByDate();
        }

    }
}
