using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PCTGCM
{
    class GameProcessManager
    {

        List<GameProcess> games = new List<GameProcess>();
        Timer t = new Timer(5000);
        List<string> signatures = new List<string>();

        public void Init()
        {

            var ty = new StreamReader("data\\games.nsgn").ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var ts in ty) { signatures.Add(ts); }

            t.Elapsed += new ElapsedEventHandler(UpdateStats);
            t.Start();
        }

        private void UpdateStats(Object o,ElapsedEventArgs elp)
        {

            var p = Process.GetProcesses();
            foreach (var cp in p)
            {

                foreach (var sign in signatures)
                {
                    if (cp.ProcessName.ToLower().Contains(sign))
                    {

                        games.Add(new GameProcess(cp));

                    }

                    if (cp.MainWindowTitle.ToLower().Contains(sign))
                    {

                        games.Add(new GameProcess(cp));

                    }
                }

            }

        }

    }
}
