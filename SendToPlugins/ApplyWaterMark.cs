using System;
using System.Collections.Generic;
using System.Text;
using CaptureItPlus.Libs;
using System.Windows.Forms;

namespace SendToPlugins
{
    public class ApplyWaterMark : ISendTo
    {
        public string Name
        {
            get { return this.GetType().Name; }
        }

        public string Text
        {
            get { return "&Watermarker"; }
        }

        public void Execute(string filename)
        {
            WaterMarkerUI waterMarker = new WaterMarkerUI(filename);
            waterMarker.Show();
        }

        public string Description
        {
            get { return "This plugin is used to apply a water mark the captured image."; }
        }

        public string Help
        {
            get { return string.Format("Apply Watermark Plugin.{0}Copyright (C) 2012 captureitplus developers. All rights reserved.", Environment.NewLine); }
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
