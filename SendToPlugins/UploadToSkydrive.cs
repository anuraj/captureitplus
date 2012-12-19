using System;
using System.Windows.Forms;
using CaptureItPlus.Libs;

namespace SendToPlugins
{
    class UploadToSkydrive : ISendTo
    {
        public string Name
        {
            get { return this.GetType().Name; }
        }

        public string Text
        {
            get { return "Skydrive"; }
        }

        public void Execute(string filename)
        {
            SendToSkydriveUI sendToSkydrive = new SendToSkydriveUI(filename);
            sendToSkydrive.ShowDialog();
        }

        public string Description
        {
            get { return "This plugin helps to upload image to Skydrive"; }
        }

        public string Help
        {
            get { return string.Format("Upload to Skydrive Plugin.{0}Copyright (C) 2012 captureitplus developers. All rights reserved.", Environment.NewLine); }
        }

        public bool IsFinished
        {
            get { return false; }
        }

        public Keys ShortcutKey
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {

        }
    }
}
