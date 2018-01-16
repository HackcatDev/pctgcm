using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;
using Newtonsoft.Json;

namespace PCTGCM
{
    public class GameProcess
    {
        private Process _p;
        private Timer timer;
        private System.Int32 seconds;
        private String title;

        public void ToggleTimer()
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }

        public GameProcess(Process p)
        {
            _p = p;
            var sgn = new StreamReader("data\\games.wlist").ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            List<string> l = new List<string>();
            foreach (var tyu in sgn)
            {
                l.Add(tyu);
            }
            var blt = new StreamReader("data\\games.blist").ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            List<string> b = new List<string>();
            foreach (var ghj in blt)
            {
                b.Add(ghj);
            }
            if (!l.Contains(_p.ProcessName.ToLower()))
            {
                if (b.Contains(_p.ProcessName))
                {
                    _p.Kill();
                }
                else
                {
                    _p = p;
                    try
                    {
                        seconds = Convert.ToInt32(JsonConvert.DeserializeObject<Dictionary<String, String>>(new StreamReader("data\\games.prop").ReadToEnd())[_p.ProcessName]);
                    }
                    catch
                    {
                        seconds = Convert.ToInt32(JsonConvert.DeserializeObject<Dictionary<string, string>>(new StreamReader("data\\games.ddt").ReadToEnd())["DefaultTime"]);
                    }
                    title = _p.MainWindowTitle;
                    timer = new Timer(1000);
                    timer.Elapsed += new ElapsedEventHandler(tmr);
                    timer.AutoReset = true;
                    timer.Start();
                }
            }
            
        }

        private void tmr(Object foo0, ElapsedEventArgs foo2)
        {
            seconds--;
            if (!(seconds > 0))
            {
                _p.Kill();
                this.timer.Stop();
            }
        }
    }
}
