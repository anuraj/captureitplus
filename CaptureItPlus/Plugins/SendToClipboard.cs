namespace CaptureItPlus.Plugins
{
    using System.Drawing;
    using System.Windows.Forms;
    using CaptureItPlus.Libs;
    using System;
    

    
    public class SendToClipboard : ISendTo
    {
        private string _fileName = string.Empty;
        private bool _isFinished;

        public string Text
        {
            get { return "&ClipBoard"; }
        }

        public void Execute(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return;
            }
            _fileName = filename;
            using (var image = Image.FromFile(filename))
            {
                Clipboard.SetImage(image);
                MessageBox.Show("Image copied to clipboard", "CaptureIt Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isFinished = true;
            }
        }

        public bool IsFinished
        {
            get { return _isFinished; }
        }

        public string Description
        {
            get
            {
                return "This plugin is used to copy the captured image to clipboard";
            }
        }

        public string Help
        {
            get
            {
                return string.Format("Send To Clipboard Plugin.{0}Copyright (C) 2011 captureitplus developers. All rights reserved.", Environment.NewLine);
            }
        }

        public Keys ShortcutKey
        {
            get { return Keys.Control & Keys.C; }
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
