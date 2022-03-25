using SoitaabScan.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoitaabScan.Model
{
    public class ClientSoitaabScan
    {
        ProgramList programList;
        DirectoryMonitoring DM;

        private bool flagChange = true;

        public delegate void AccountHandler(string message);

        public event AccountHandler ProgrammStarted;
                
        public ClientSoitaabScan(ref string path, ref string IgnorFolder)
        {
            programList = new ProgramList(new DirectoryInfo(path), ref IgnorFolder);
            DM = new DirectoryMonitoring(path);
        }
        public void ClientStart()
        {
            ProgrammStarted.Invoke("Program started , waite please");
            programList.GetListAllPrograms();
            StartWatching();
        }

        public string GetListPrograms()
        {
            if (flagChange == true)
            {
              flagChange = false;
              return programList.GetStringInfoAllPrograms();
            }
            return programList.res;
        }

        public string GetProgramsOnlyOneThickness(int thicknes)
        {
            return programList.GetProgramsOnlyOneThickness(thicknes);
        }

        public string SortByDate()
        {
            return programList.SortByDate();
        }
        public void StartWatching()
        {
            DM.changesEvent += Watch_changesEvent;
        }

        private void Watch_changesEvent()
        {
            flagChange = true;
        }
    }
}
