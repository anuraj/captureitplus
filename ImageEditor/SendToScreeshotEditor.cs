using CaptureItPlus.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageEditor
{
    class SendToScreeshotEditor : ISendTo
    {
        public string Name
        {
            get { return "Screeshot Editor"; }
        }

        public string Text
        {
            get { return "Screeshot Editor"; }
        }

        public void Execute(string filename)
        {
            using (FrmMainUI frmMainUI = new FrmMainUI(filename))
            {
                frmMainUI.ShowDialog();
            }
        }

        public ISendToHost Host
        {
            get;
            set;
        }

        public void Dispose()
        {
            //nothing
        }
    }
}
