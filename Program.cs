using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;
using Newtonsoft.Json;

namespace PCTGCM
{
    public class Program
    {
        
        public static void Main()
        {

            GameProcessManager gpm = new GameProcessManager();
            gpm.Init();
            Console.ReadKey();

        }

    }
}
