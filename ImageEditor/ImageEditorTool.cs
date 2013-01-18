using System;
using System.Collections.Generic;
using System.Text;
using CaptureItPlus.Libs;

namespace ImageEditor
{
    public class ImageEditorTool : ITool
    {
        public void Execute(params object[] args)
        {
            var frmMainUi = new FrmMainUI();
            frmMainUi.Show();
        }

        public string Name
        {
            get { return "Screenshot Editor"; }
        }

        public string Description
        {
            get { return "Screenshot Editor"; }
        }

        public string MenuCaption
        {
            get { return "Screenshot Editor"; }
        }

        public bool IsVisible
        {
            get { return true; }
        }
    }
}
