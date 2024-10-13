﻿using System;
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
            var path = AppDomain.CurrentDomain.BaseDirectory + "Visualization\\U3DVisualization.exe";
            _process = new Process
            {
                StartInfo =
                {
                    FileName = AppDomain.CurrentDomain.BaseDirectory + "Visualization\\BallGame.exe",
                    Arguments = "-parentHWND " + Handle.ToInt32() + " " + Environment.CommandLine,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    //WindowStyle = ProcessWindowStyle.Hidden
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
