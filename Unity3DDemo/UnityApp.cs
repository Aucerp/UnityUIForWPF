using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unity3DDemo
{
    public class UnityApp: Control
    {
        private Process _process;

        public UnityApp()
        {
            StartUnityApp();
        }

        private void StartUnityApp()
        {
            _process = new Process
            {
                StartInfo =
                {
                    FileName = AppDomain.CurrentDomain.BaseDirectory + "Visualization\\UnityUIForWPF.exe",
                    Arguments = "-parentHWND " + Handle.ToInt32() + " " + Environment.CommandLine,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                }
            };

            _process.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (_process != null)
            {
                _process.Kill();
                _process.Dispose();
                _process = null;
            }

            base.Dispose(disposing);
        }
    }
}
