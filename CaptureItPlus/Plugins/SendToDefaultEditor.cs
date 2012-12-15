namespace CaptureItPlus.Plugins
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using CaptureItPlus.Libs;
    

    
    public class SendToDefaultEditor : ISendTo
    {
        public string Text
        {
            get { return "Default &Editor"; }
        }

        public void Execute(string filename)
        {
            string executable = FindExecutable(filename);
            if (executable != null)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(executable);
                processStartInfo.Arguments = filename;
                Process.Start(processStartInfo);
                IsFinished = true;
            }
        }

        public string Description
        {
            get
            {
                return "This plugin is used to open the captured image in the default image editor";
            }
        }

        public string Help
        {
            get
            {
                return string.Format("Send To Default Editor Plugin.{0}Copyright (C) 2011 captureitplus developers. All rights reserved.", Environment.NewLine);
            }
        }

        public bool IsFinished
        {
            get;
            private set;
        }

        public Keys ShortcutKey
        {
            get { return Keys.None; }
        }

        [DllImport("shell32.dll", EntryPoint = "FindExecutable")]
        public static extern long FindExecutableA(string lpFile, string lpDirectory, StringBuilder lpResult);

        public static string FindExecutable(string pv_strFilename)
        {
            StringBuilder objResultBuffer = new StringBuilder(1024);
            long lngResult = 0;

            lngResult = FindExecutableA(pv_strFilename, string.Empty, objResultBuffer);

            if (lngResult >= 32)
            {
                return objResultBuffer.ToString();
            }

            return null;
        }

        public string Name
        {
            get { return this.GetType().Name; }
        }

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}
